using System.Collections;
using System.Data.Common;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody2D playerBody;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float decreaseFireDelay;
    public float fireDelay;
    private bool volleyActive = false;
    public float maxHealth;
    public float healAmount;
    public static float currentHealth;
    public HealthBarScript healthBar;
    public float damage;
    
    //Von Lilli
    private float old_volume;
    [SerializeField]
    public GameManager gameManager;
    private bool isDead;
    public InputManager inputManager;

    //Audio
    [SerializeField]

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager = FindFirstObjectByType<GameManager>();
        inputManager = FindFirstObjectByType<InputManager>();
    }
    void Update()
    {
        float horizontal = 0, vertical = 0;
        if (Input.GetKey(inputManager.forward))
        {
            vertical = 1;
        }
        if (Input.GetKey(inputManager.backward))
        {
            vertical = -1;
        }
        if (Input.GetKey(inputManager.right))
        {
            horizontal = 1;
        }
        if (Input.GetKey(inputManager.left))
        {
            horizontal = -1;
        }

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
        playerBody.velocity = movement * playerSpeed;

        if (horizontal > 0)
        {
            GetComponent<Animator>().Play("Right");
        }
        else if (horizontal < 0)
        {
            GetComponent<Animator>().Play("Left");
        }
        else if (vertical > 0)
        {
            GetComponent<Animator>().Play("Up");
        }
        else if (vertical < 0)
        {
            GetComponent<Animator>().Play("Down");
        }

        float shootHor = 0, shootVer = 0;
        if (Input.GetKey(inputManager.fireForward))
        {
            shootVer = 1;
        }
        if (Input.GetKey(inputManager.fireBackward))
        {
            shootVer = -1;
        }
        if (Input.GetKey(inputManager.fireRight))
        {
            shootHor = 1;
        }
        if (Input.GetKey(inputManager.fireLeft))
        {
            shootHor = -1;
        }
        if ((shootHor != 0 || shootVer != 0) &&
             Time.time > lastFire + fireDelay &&
             currentHealth > 0 &&
             !volleyActive)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;
        }

    }

    public void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) : Mathf.Ceil(x),
            (y < 0) ? Mathf.Floor(y) : Mathf.Ceil(y),
            0
        ).normalized * bulletSpeed;
        AudioManager.Instance.PlaySfx("VolleySound");

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            gameManager.gameOver();
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        healthBar.SetHealth(currentHealth);
        AudioManager.Instance.PlaySfx("HealthSound");
    }
    public void IncreaseFireRate()
    {
        fireDelay -= decreaseFireDelay;
        StartCoroutine(ResetFireRateAfterDelay(5));
        AudioManager.Instance.PlaySfx("FirerateSound");
    }

    private IEnumerator ResetFireRateAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        fireDelay += decreaseFireDelay;
    }

    private void StartBulletVolley()
    {
        volleyActive = true;
        int iterations = 6;
        old_volume =AudioManager.Instance.GetSFXVolume();
        Debug.Log("SFX Volume: "+ AudioManager.Instance.GetSFXVolume() );
        if (old_volume>0.4f){
            AudioManager.Instance.SfxVolume(0.4f);
        Debug.Log("NEWWWW SFX Volume: "+ AudioManager.Instance.GetSFXVolume() );
        }
        AudioManager.Instance.PlaySfx("VolleySound");
        StartCoroutine(BulletVolley(iterations));
        
    }

    private IEnumerator BulletVolley(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            Shoot(0, 1);
            Shoot(1, 0);
            Shoot(1, 1);
            Shoot(0, -1);
            Shoot(-1, 0);
            Shoot(-1, 1);
            Shoot(1, -1);
            Shoot(-1, -1);
            yield return new WaitForSeconds(fireDelay);
        }
        volleyActive = false;
        AudioManager.Instance.SfxVolume(old_volume);
        Debug.Log("SFX Volume After: "+ AudioManager.Instance.GetSFXVolume() );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            string itemID = collision.GetComponent<ItemController>().ID;
            if (itemID == "health")
            {
                Heal(healAmount);
            }
            if (itemID == "fireRate")
            {
                IncreaseFireRate();
            }
            if (itemID == "volley")
            {
                StartBulletVolley();
            }
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
        }
    }

    private void Die()
    {
        Destroy(gameObject); 
        Debug.Log("Player died!");
    }

}
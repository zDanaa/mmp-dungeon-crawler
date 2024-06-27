using System.Collections;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody2D playerBody;
    public Text collectedText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay = 0.5f;
    private Coroutine fireRateCoroutine;

    private bool volleyActive = false;

    public float maxHealth = 100;
    public float currentHealth;
    public HealthBarScript healthBar;
    // Start is called before the first frame update

    //Von Lilli
    [SerializeField]
    private int maxHit; // Wie oft er von bullets getroffen werden kann

    private int health = 5;
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth >0 ){}
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;

        if (horizontal > 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
            playerBody.velocity = movement * playerSpeed;
            collectedText.text = "Item Collected:" + collectedAmount;

            if (horizontal > 0)
            {
                GetComponent<Animator>().Play("Right");
            } else if (horizontal < 0){
                GetComponent<Animator>().Play("Left");
            } else if (vertical > 0){
                GetComponent<Animator>().Play("Up");
            } else if (vertical < 0){
                GetComponent<Animator>().Play("Down");
            }
        }
        float shootHor = Input.GetAxisRaw("ShootHorizontal");
        float shootVer = Input.GetAxisRaw("ShootVertical");

        if ((shootHor != 0 || shootVer !=0) && 
             Time.time > lastFire + fireDelay && 
             currentHealth >0 && 
             !volleyActive)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;
        }

        //test method for the heathbar, to be deleted later !!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }


    }
    public void Shoot (float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x<0) ? Mathf.Floor(x) : Mathf.Ceil(x),
            (y<0) ? Mathf.Floor(y) : Mathf.Ceil(y),
            0
        ).normalized * bulletSpeed;
    }


    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth){currentHealth = maxHealth;}
        healthBar.SetHealth(currentHealth);
    }
    public void IncreaseFireRate()
    {
        if (fireRateCoroutine != null)
        {
            StopCoroutine(fireRateCoroutine);
        }
        fireDelay -= 0.2f;
        fireRateCoroutine = StartCoroutine(ResetFireRateAfterDelay(5));
    }

    private IEnumerator ResetFireRateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fireDelay = 0.5f;
    }

    private void StartBulletVolley(){
        volleyActive = true;
        int iterations = 6;
        StartCoroutine(BulletVolley(iterations));
    }

    private IEnumerator BulletVolley(int iterations){
        for (int i = 0; i < iterations; i++){
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item"){
            string itemID = collision.GetComponent<ItemController>().ID;
            if (itemID  == "health"){
                Heal(40);
            }
            if (itemID == "fireRate"){
                IncreaseFireRate();
            }
            if (itemID == "volley"){
                StartBulletVolley();
            }
        }
        if(collision.gameObject.CompareTag("EnemyBullet")){
            maxHit -=1;
            if(maxHit == 0){
            Destroy(gameObject); //Or death animation
            Debug.Log("killed");
            }
        }
    }

    void Die()
    {
        // Handle player death (e.g., respawn, game over, etc.)
        Debug.Log("Player died!");
    }
    

}

/*if(other.gameObject.CompareTag("Enemy")){
            Destroy(other.gameObject);
            target = null;
        } */ //Nach dem prinzip könnte man One Hit enemys machen
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
    public float fireDelay;
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBarScript healthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth >0 )
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

        if ((shootHor != 0 || shootVer !=0) && Time.time > lastFire + fireDelay && currentHealth >0)
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
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth){currentHealth = maxHealth;}
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item"){
            Heal(40);
        }
    }
    
}

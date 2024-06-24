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
    // Start is called before the first frame update

    //Von Lilli
    [SerializeField]
    private int maxHit; // Wie oft er von bullets getroffen werden kann
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;

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
    
        float shootHor = Input.GetAxisRaw("ShootHorizontal");
        float shootVer = Input.GetAxisRaw("ShootVertical");

        if ((shootHor != 0 || shootVer !=0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;
        }

        playerBody.velocity = movement * playerSpeed;
        collectedText.text = "Item Collected:" + collectedAmount;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EnemyBullet")){
            maxHit -=1;
            if(maxHit == 0){
            Destroy(gameObject); //Or death animation
            Debug.Log("killed");
            }
        }
    }
}

/*if(other.gameObject.CompareTag("Enemy")){
            Destroy(other.gameObject);
            target = null;
        } */ //Nach dem prinzip könnte man One Hit enemys machen
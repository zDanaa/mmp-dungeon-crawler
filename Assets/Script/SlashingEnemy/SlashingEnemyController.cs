using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashingEnemyController : MonoBehaviour
{
    [SerializeField]

    public Transform player;
    private Rigidbody2D rb;
    public float speed = 1f;
    public int damage = 10;
    public float attackCooldown = 2.0f; // Time between attacks
    public float stopDistance = 0.5f; // Distance to stop from player
    private bool canAttack = true;
    private Animator animator;
    public HealthBarScript healthBar;
    public float maxHealth = 100;
    public float currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        //target= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  
        animator = GetComponent<Animator>(); 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null){
            //PlayerController destroyed
            return;
        }
        
        //To move enemy to player
       /* if(Vector2.Distance(transform.position, target.position)> distanceToPlayer ){
        transform.position= Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime );
        }
        //Near enough but not to near he is supoosed to stop
        /*else if (Vector2.Distance(transform.position, target.position)< distanceToPlayer && Vector2.Distance(transform.position, target.position)> retreatDistance){
            transform.position=this.transform.position;
        }*/
        
        //Follows player up to a certain range and then attacks
        if (player != null) {
            
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance > stopDistance)
            {
                animator.SetBool("isAttacking", false);
                Vector2 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else if (canAttack){
                Debug.Log("isAttacking " + animator.GetBool("isAttacking"));
                
                StartCoroutine(Attack());
            }
        }

    }

    //Method to attack
     IEnumerator Attack()
    {
        canAttack = false;
        animator.SetBool("isAttacking", true);
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null)
        {
            
            playerScript.TakeDamage(damage);
            
        }
        
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("isAttacking", false);
        canAttack = true;
    }

    // Enemy takes damage when it collides with  player bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(20);
        } 
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) 
        { 
           Destroy(gameObject);
           Debug.Log("Demon killed");
        }
    }


    
}

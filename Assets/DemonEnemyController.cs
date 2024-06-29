using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemyController : MonoBehaviour
{

    
    public Transform player;
    private Rigidbody2D rb;
    public float speed = 1f;
    public int damage = 10;
    public float attackCooldown = 2.0f;
    public float stopDistance = 0.5f;
    private bool canAttack = true;

    //public float attackDistance = 0.5f;
    //public int playerHealth = 5;
    //public float bounceBackForce = 2f;
    //private Animator animator;
    
    //private bool isSlashing = false;

    public HealthBarScript healthBar;
    public float maxHealth = 100;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
       // animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update() 
    
    {
        //Move towards player
        /* Vector2 direction = (player.position - transform.position).normalized;
         float distance = Vector2.Distance(player.position, transform.position);

         if (distance > attackDistance)
         {
             rb.velocity = direction * speed;

         }
         else
         {
             rb.velocity = Vector2.zero;
             animator.SetTrigger("isSlashing");
         }*/
        /* if (!isSlashing)
        {
            FollowPlayer();
        }*/
        //FollowPlayer();
        if (player != null) {

            float distance = Vector2.Distance(transform.position, player.position);
            if (distance > stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else if (canAttack){
                StartCoroutine(Attack());
            }
        }
       

    }

    /* void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
             // Bounce back
             Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
             rb.AddForce(bounceDirection * speed, ForceMode2D.Impulse);

             // Damage the player
             playerHealth -= 1;
             if (playerHealth <= 0)
             {
                 Destroy(collision.gameObject);
             }
         }



         void OnCollisionEnter2D(Collision2D collision)
         {
             if (collision.gameObject.CompareTag("Player"))
             {
                 // Bounce back
                 Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
                 rb.AddForce(bounceDirection * speed, ForceMode2D.Impulse);

                 // Damage the player
                 PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                 if (playerHealth != null)
                 {
                     playerHealth.TakeDamage(1);
                 }
             }
         }*/
    /*void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float distance = Vector2.Distance(player.position, transform.position);

            if (distance > attackDistance)
            {
                rb.velocity = direction * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                //StartSlashing();
            }
        }
    }

   /* void StartSlashing()
    {
        isSlashing = true;
        animator.SetTrigger("isSlashing");
    }*/

   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(bounceDirection * bounceBackForce, ForceMode2D.Impulse);

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                Debug.Log("Player Health minus slashing: " + playerHealth.CurrentHealth);
            }
            //isSlashing = false;
            Invoke("ResumeFollowingPlayer", 0.5f);
        }
    }

    /*public void EndSlashing()
    {
        isSlashing = false;
    }*/

   /* void ResumeFollowingPlayer() {
        rb.velocity = Vector2.zero;
        FollowPlayer();
    }*/



   

     /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(20);
            Debug.Log("Demon hit by bullet");
        } 
        if (collision.CompareTag("Player") && canAttack)
        {
            PlayerController playerScript = collision.GetComponent<PlayerController>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
                StartCoroutine(AttackCooldown());
            }
        }

    }*/
     IEnumerator Attack()
    {
        canAttack = false;
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage);
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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

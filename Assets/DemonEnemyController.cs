using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;
    public float attackDistance = 0.5f;
    //public int playerHealth = 5;
    public float bounceBackForce = 2f;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isSlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
         if (!isSlashing)
        {
            FollowPlayer();
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
    void FollowPlayer()
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
                StartSlashing();
            }
        }
    }

    void StartSlashing()
    {
        isSlashing = true;
        animator.SetTrigger("isSlashing");
    }

    void OnCollisionEnter2D(Collision2D collision)
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
            isSlashing = false;

        }
    }

    public void EndSlashing()
    {
        isSlashing = false;
    }


}

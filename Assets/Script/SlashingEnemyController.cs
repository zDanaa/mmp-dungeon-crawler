using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashingEnemyController : MonoBehaviour
{
    [SerializeField]

    private Rigidbody2D rb;
    public Transform player;
    public float speed = 1f;
    public int damage = 10;
    public float attackCooldown = 2.0f; 
    public float stopDistance = 0.5f;
    private bool canAttack = true;
    private Animator animator;
    public HealthBarScript healthBar;
    public float maxHealth = 100;
    public float currentHealth;

    void Start()
    {  
        animator = GetComponent<Animator>(); 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    void Update()
    {
        if (player == null){
            return;
        }
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashingEnemyController : EnemyController
{
    [SerializeField]
    private Rigidbody2D rb;
    public float attackCooldown = 2.0f; 
    public float stopDistance = 0.5f;
    private bool canAttack = true;
    private Animator animator;
    void Start()
    {  
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();      
    }
    void Update()
    {
        if (target == null){
            return;
        }
         
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > stopDistance)
        {
            animator.SetBool("isAttacking", false);
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (canAttack){            
            StartCoroutine(Attack());
        }
    }
     IEnumerator Attack()
    {
        canAttack = false;
        animator.SetBool("isAttacking", true);
        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage); 
        }
        
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("isAttacking", false);
        canAttack = true;
    }
}

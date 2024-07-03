using System.Collections;
using UnityEngine;

public class SlashingEnemyController : EnemyController
{
    [SerializeField] private Rigidbody2D rb;
    public float attackCooldown; 
    public float attackDuration;
    public float meleeRange; 
    public float aggroRange;
    private bool isPlayerInRange;
    private PlayerController player;
    private float attackTimer;
    private bool isAttacking = false;

    void Start()
    {  
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<PlayerController>();
        animator = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
        attackTimer = attackCooldown;
    }

    void Update()
    {
        if (target == null) return;

        attackTimer += Time.deltaTime;
        isPlayerInRange = Vector2.Distance(transform.position, target.position) <= aggroRange;
        
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;
        setSpriteFlip(direction);

        if (distance > meleeRange && !isAttacking && isPlayerInRange)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            ChasePlayer(direction);
        }
        if (attackTimer >= attackCooldown && !isAttacking && distance <= meleeRange)
        {   
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            Attack();
        }
        if (!isPlayerInRange)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }
    }

    void ChasePlayer(Vector2 direction)
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    void Attack()
    {
        isAttacking = true;
        attackTimer = 0f;
        animator.SetBool("Attack", true); 

        if (player != null)
        {
            player.TakeDamage(damage); 
        }

        isAttacking = false;
        animator.SetBool("Attack", false);
    }
}
using System.Collections;
using UnityEngine;

public class SlashingEnemyController : EnemyController
{
    [SerializeField]
    public float attackDuration;
    public float meleeRange; 
    private PlayerController player;
    private bool isAttacking = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<PlayerController>();
        animator = GetComponent<Animator>(); 
        attackTimer = attackCooldown;
    }

    void Update()
    {
        if (target == null) return;

        attackTimer += Time.deltaTime;
        bool isPlayerInRange = Vector2.Distance(transform.position, target.position) <= aggroRange;
        
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
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        attackTimer = 0;
        isAttacking = true;
        animator.SetBool("Attack", true); // Start the attack animation

        yield return new WaitForSeconds(attackDuration); // Wait for the animation to play through

        player.TakeDamage(damage);

        animator.SetBool("Attack", false); // End the attack animation
        yield return new WaitForSeconds(attackCooldown - attackDuration); // Cooldown before next attack

        isAttacking = false;
    }
}
using System.Collections;
using UnityEngine;

public class SlashingEnemyController : EnemyController
{
    [SerializeField]
    public float attackDuration;
    public float meleeRange; 
    private bool isAttacking = false;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<PlayerController>();
        playerDamage = player.damage;
        attackTimer = attackCooldown;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        attackTimer += Time.deltaTime;
        
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;
        setSpriteFlip(direction, 1);

        if (distance > meleeRange && !isAttacking && distance <= aggroRange)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            ChasePlayer(direction);
        }
        if (attackTimer >= attackCooldown && !isAttacking && distance <= meleeRange)
        {   
            Attack();
        }
        if (distance > aggroRange)
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
        animator.SetBool("Attack", true); 

        yield return new WaitForSeconds(attackDuration); 

        player.TakeDamage(damage);

        animator.SetBool("Attack", false); 
        yield return new WaitForSeconds(attackCooldown - attackDuration); 

        isAttacking = false;
    }
}
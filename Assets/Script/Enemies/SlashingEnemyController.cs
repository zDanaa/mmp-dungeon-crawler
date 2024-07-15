using System.Collections;
using UnityEngine;

public class SlashingEnemyController : EnemyController
{
    [SerializeField]
    public float attackDuration;
    public float meleeRange; 
    private bool isAttacking = false;
    protected override void Start()
    {
        base.Start();
        attackTimer = attackCooldown;
        initialFlip = 1;
    }

    protected override void Update()
    {
        base.Update();

        attackTimer += Time.deltaTime;

        if (distanceToPlayer > meleeRange && !isAttacking && distanceToPlayer <= aggroRange)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            ChasePlayer(direction);
        }
        if (attackTimer >= attackCooldown && !isAttacking && distanceToPlayer <= meleeRange)
        {   
            Attack();
        }
        if (distanceToPlayer > aggroRange)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }
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
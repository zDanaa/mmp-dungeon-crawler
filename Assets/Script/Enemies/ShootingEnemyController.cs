using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyController : EnemyController
{

    [SerializeField]
    public float dangerZone;
    public float safeZone;
    public GameObject bullet;

    protected override void Start()
    {
        base.Start();
        attackTimer = attackCooldown;
        initialFlip = -1;
    }
    protected override void Update()
    {
        base.Update();

        if (distanceToPlayer > aggroRange) 
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }
        else if (transform != null){
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            if (distanceToPlayer > safeZone)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (distanceToPlayer < dangerZone)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }
            else if (distanceToPlayer < safeZone && distanceToPlayer > dangerZone + 0.5)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, (speed / 2) * Time.deltaTime);
            }

            attackTimer += Time.deltaTime;
            if (attackTimer > attackCooldown)
            {
                attackTimer = 0;
                animator.SetTrigger("Attack");
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}





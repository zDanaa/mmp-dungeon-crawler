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

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (PlayerController.currentHealth > 0)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

    }
    void Update()
    {
        if (target == null) return;

        setSpriteFlip(target.position - transform.position, -1);

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget > aggroRange) 
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }
        else {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            if (distanceToTarget > safeZone)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (distanceToTarget < dangerZone)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }
            else if (distanceToTarget < safeZone && distanceToTarget > dangerZone)
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





using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlhoonBossController : EnemyController
{

    [SerializeField]
    public float dangerZone;
    public float safeZone;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletHellCooldown;
    public float bulletHellDuration;
    private float bulletHellTimer;
    public float healCooldown;
    public float healDuration;
    private float healTimer;
    public float healAmount;
    private bool isHealing; 
    private bool nextMoveIsBulletHell;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isHealing = false;
        if (PlayerController.currentHealth > 0)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

    }
    void Update()
    {
        if (target == null) return;

        setSpriteFlip(target.position - transform.position, 1);

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget > aggroRange) 
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }
        else {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);

            if (!isHealing)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            attackTimer += Time.deltaTime;
            if (attackTimer > attackCooldown && !isHealing)
            {
                attackTimer = 0;
                animator.SetTrigger("Attack");
                Shoot();            
            }

            bulletHellTimer += Time.deltaTime;
            if (bulletHellTimer > bulletHellCooldown && !isHealing && nextMoveIsBulletHell)
            {
                StartCoroutine(BulletHell());
                nextMoveIsBulletHell = false;
                bulletHellTimer = 0;
                healTimer = 0;
            }

            healTimer += Time.deltaTime;
            if (healTimer > healCooldown && !isHealing && !nextMoveIsBulletHell)
            {
                StartCoroutine(Heal());
                nextMoveIsBulletHell = true;
                healTimer = 0;
                bulletHellTimer = 0;
            }
        }
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    IEnumerator BulletHell()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Heal", false);
        animator.SetBool("BulletHell", true);
        int directions = 8; 
        float angleStep = 360f / directions; 
        float currentAngle = 0f; 

        for (int i = 0; i < bulletHellDuration; i++) 
        {
            for (int j = 0; j < directions; j++)
            {
                float angle = currentAngle + (j * angleStep);
                
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
                rb.velocity = direction * bulletSpeed;
            }
            currentAngle += angleStep / directions; 
            yield return new WaitForSeconds(0.1f);
        }
        animator.SetBool("BulletHell", false);
    }
    IEnumerator Heal()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("BulletHell", false);
        animator.SetBool("Heal", true);
        isHealing = true;
        for (int i = 0; i < healDuration; i++)
        {
            currentHealth += healAmount;
            healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds(0.5f);
        }
        isHealing = false;
        animator.SetBool("Heal", false);
    }
}





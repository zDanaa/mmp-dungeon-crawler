using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public HealthBarScript healthBar;
    public Transform target;
    public float distanceToPlayer;
    public Vector2 direction;
    public SpriteRenderer spriteRenderer;
    protected int initialFlip;
    public PlayerController player;
    public Animator animator;
    public float speed;
    public float attackTimer;
    public float attackCooldown;
    public float aggroRange;
    public float damage;
    public float playerDamage;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<PlayerController>();
        playerDamage = player.damage;
        attackTimer = attackCooldown;
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        if (target == null) return;
        
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        direction = (target.position - transform.position).normalized;
        setSpriteFlip(direction, initialFlip);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(playerDamage);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setSpriteFlip(Vector2 direction, int initialFlip)
    {
        direction = initialFlip * direction;
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void ChasePlayer(Vector2 direction)
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
}




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
    public SpriteRenderer spriteRenderer;
    public PlayerController player;
    public Animator animator;
    public float speed;
    public float attackTimer;
    public float attackCooldown;
    public float aggroRange;
    public float receiveDamage;
    public float damage;

    void Start()
    {

    }
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(receiveDamage);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= receiveDamage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setSpriteFlip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}




using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{

    [SerializeField]
    public float speed;
    private Transform target; 

    [SerializeField]
    private float minimumDistanceToPlayer;

    [SerializeField]
    private float retreatDistance;
    public HealthBarScript healthBar;
    public float maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (target == null)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > minimumDistanceToPlayer)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToTarget < retreatDistance)
        {
            RetreatFromPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void RetreatFromPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(20);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) { Die(); }
    }

    private void Die()
    {
        Destroy(gameObject); 
    }
}




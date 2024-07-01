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
    public float speed;

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




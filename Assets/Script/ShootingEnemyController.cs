using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyController : EnemyController
{

    [SerializeField]
    private float minimumDistanceToPlayer;

    [SerializeField]
    private float retreatDistance;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 1f;
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
}




using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyController : EnemyController
{

    [SerializeField]
    public float dangerZone;
    public float safeZone;
    public float fireRate;
    public float timer;
    public GameObject bullet;
    public Transform bulletPos;

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
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed/2) * Time.deltaTime); 
    }

    timer += Time.deltaTime;
    if (timer > fireRate){
        timer = 0;
        Shoot();
    }
    }

    public void Shoot(){
        Instantiate(bullet,transform.position, Quaternion.identity);
    }
}





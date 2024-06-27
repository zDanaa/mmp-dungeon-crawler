using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{

    //Attributs needed to follow player
    [SerializeField]
    public float speed;
    private Transform target; //Our Player

    [SerializeField]
    private float distanceToPlayer;

    [SerializeField]
    private float retreatDistance;

    //private Rigidbody2D rb;
    public HealthBarScript healthBar;
    public float maxHealth = 100;
    public float currentHealth;





    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // Plaxer was destroyed so just stop
            return;
        }

        //To move enemy to player
        if (Vector2.Distance(transform.position, target.position) > distanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        //Near enough but not to near he is supoosed to stop
        else if (Vector2.Distance(transform.position, target.position) < distanceToPlayer && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        //Enemy is supposed to retreat
        if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }

    }



    //Selbstmord begehen und Spieler auch töten
    /*private void OnCollisionEnter2D (Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            Destroy(other.gameObject);
            target = null;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy hit by bullet (trigger)");
            Destroy(gameObject);
        }
    }
}




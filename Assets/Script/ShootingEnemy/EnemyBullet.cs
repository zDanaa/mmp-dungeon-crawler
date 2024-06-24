using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb; 
    
    [SerializeField]
    private float force;

    private float timer;

    void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag ("Player");
        
        if(player != null){
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized*force;

        float rotate= Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotate+90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10){
            Destroy(gameObject);
        } 
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Player hit by bullet (trigger)");
        }
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //Attributs
    [SerializeField]
    private float speed;

    private Transform player;

    private Vector2 target;
    

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 

        if(player != null){
        target = new Vector2 ( player.position.x, player.position.y );
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
         transform.position = Vector2.MoveTowards(transform.position, player.position, speed* Time.deltaTime );
        }
        if((Vector2)transform.position == target){
            DestroyBullet();
        }
    }

 public void OnTriggerEnter2D (Collider2D other){
        if(other.CompareTag("Player")) {
            DestroyBullet();
        }
    }
    public void DestroyBullet(){
        Destroy(gameObject);
    }

   
}

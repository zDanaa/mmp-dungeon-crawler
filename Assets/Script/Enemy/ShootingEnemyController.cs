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




    // Start is called before the first frame update
    void Start()
    {
        target= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   

       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector2.Distance(transform.position, target.position)> distanceToPlayer ){
        transform.position= Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime );
        }
    }
           
           
}

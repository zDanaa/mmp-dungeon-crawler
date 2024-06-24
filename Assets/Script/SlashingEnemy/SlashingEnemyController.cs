using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashingEnemyController : MonoBehaviour
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
        if (target == null){
            //PlayerController destroyed
            return;
        }
        
        //To move enemy to player
        if(Vector2.Distance(transform.position, target.position)> distanceToPlayer ){
        transform.position= Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime );
        }
        //Near enough but not to near he is supoosed to stop
        /*else if (Vector2.Distance(transform.position, target.position)< distanceToPlayer && Vector2.Distance(transform.position, target.position)> retreatDistance){
            transform.position=this.transform.position;
        }*/
        
       

    }

    
}

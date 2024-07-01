using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;
    [SerializeField]
    private float fireRate = 2;

    private float timer;

    //if you want the enemy to have only a certain shooting distance use this (3 comments)
    private GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
        //if you want the enemy to have only a certain shooting distance use this
        player = GameObject.FindGameObjectWithTag ("Player");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
        //Debug.Log("Shoot");
        timer += Time.deltaTime;

        //if you want the enemy to have only a certain shooting distance use this
        /*float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < 10){
            timer += Time.deltaTime;

            if (timer > 2){
            timer = 0;
            shoot();

        }*/
            
        if (timer > fireRate){
            timer = 0;
            shoot();

        }
        }
    }

    public void shoot(){
        Instantiate(bullet,bulletPos.position, Quaternion.identity);
        
    }
}

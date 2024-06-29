using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : ItemController
{

    

    private AudioSource myAudioSource;
    private CircleCollider2D itemCollider;
    private SpriteRenderer sr;


 void Start(){
        myAudioSource = GetComponent<AudioSource>();
        itemCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>(); 
    }
    public FireRatePowerUp()
    {
        ID  = "fireRate";
    }

     private void OnTriggerEnter2D(UnityEngine.Collider2D col)
    {
       
        if (col.gameObject.tag == "Player" ){
            myAudioSource.Play();
            itemCollider.enabled = false;
            sr.enabled= false;
            Debug.Log("Played health sound on collision with " + col.gameObject.tag);
        }
    }
}

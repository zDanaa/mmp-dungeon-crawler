using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPowerUp : ItemController
{
    

    
    private AudioSource myAudioSource;
    private CircleCollider2D itemCollider;
    private SpriteRenderer sr;

    

    

    void Start(){
        myAudioSource = GetComponent<AudioSource>();
        itemCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();    
    }
    public HealthPowerUp()
    {
        ID  = "health";
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

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPowerUp : ItemController
{
    

    [SerializeField]
    private AudioSource myAudioSource;
    private CircleCollider2D itemCollider;
    private SpriteRenderer sr;

    

    

    void Start(){
        myAudioSource = GetComponent<AudioSource>();
        Debug.Log("Ich komme hier hin");
        itemCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();


        
    }
    public HealthPowerUp()
    {
        ID  = "health";
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D col)
    {
       
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet"){
            myAudioSource.Play();
            itemCollider.enabled = false;
            sr.enabled= false;
            Debug.Log("Played health sound on collision with " + col.gameObject.tag);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string ID;


    //For sound
     private AudioSource myAudioSource;
     private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start(){
        myAudioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player"){
            PlayerController.collectedAmount++;
            sr.enabled= false;
            myAudioSource.Play();
            StartCoroutine(DestroyAfterSound());
        }
    }

     private IEnumerator DestroyAfterSound()
    {
        // Waite untill sound is played
       yield return new WaitForSeconds(myAudioSource.clip.length);
       Destroy(gameObject);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}

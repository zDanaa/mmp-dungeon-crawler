using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string ID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player"){
            PlayerController.collectedAmount++;
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}

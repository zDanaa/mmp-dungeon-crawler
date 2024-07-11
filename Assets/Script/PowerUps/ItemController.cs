using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string ID;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sr.enabled = false;
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        StartCoroutine(DeathDelay());
    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle"){
            Destroy(gameObject);
        }
    }
}

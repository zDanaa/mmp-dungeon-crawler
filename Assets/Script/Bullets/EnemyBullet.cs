using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField]
    private float force;
    public float lifeTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

            float rotate = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotate + 90);
        }
        StartCoroutine(DeathDelay());
        AudioManager.Instance.PlaySfx("EnemyBullet");
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }
}

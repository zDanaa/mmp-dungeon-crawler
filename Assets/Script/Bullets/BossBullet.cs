using UnityEngine;

public class BossBullet : Bullet
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        base.OnTriggerEnter2D(collision);
    }
}
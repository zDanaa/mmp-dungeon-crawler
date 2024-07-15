using System.Collections;
using UnityEngine;

public class ExplodingEnemyController : EnemyController
{
    [SerializeField]
    public float explosionRadius; 
    public float explosionDelay;
    private bool isExploding = false;
    public GameObject explosionPrefab;
    protected override void Start()
    {
        base.Start();
        initialFlip = 1;
    }

    protected override void Update()
    {
        base.Update();
        if (distanceToPlayer > aggroRange && !isExploding)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);

        }
        if (distanceToPlayer < aggroRange && !isExploding)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            ChasePlayer(direction);
        }
        if (distanceToPlayer < explosionRadius && !isExploding)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetTrigger("Attack");
            Explode();
        }
    }

    void Explode()
    {
        StartCoroutine(ExplodeRoutine());
    }

    IEnumerator ExplodeRoutine()
    {
        isExploding = true;
        yield return new WaitForSeconds(explosionDelay);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        if (explosionAnimator != null)
        {
            AnimatorStateInfo stateInfo = explosionAnimator.GetCurrentAnimatorStateInfo(0);
            Destroy(explosion, stateInfo.length);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Collider tag: " + collider.tag);
            if (collider.tag == "Player")
            {
                player.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    
}
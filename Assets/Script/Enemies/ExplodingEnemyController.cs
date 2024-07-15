using System.Collections;
using UnityEngine;

public class ExplodingEnemyController : EnemyController
{
    [SerializeField]
    public float explosionRadius; 
    public float explosionDelay;
    private bool isExploding = false;
    public GameObject explosionPrefab;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<PlayerController>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (target == null) return;
        
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;
        setSpriteFlip(direction, 1);

        if (distance > aggroRange && !isExploding)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);

        }
        if (distance < aggroRange && !isExploding)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            ChasePlayer(direction);
        }
        if (distance < explosionRadius && !isExploding)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetTrigger("Attack");
            Explode();
        }
    }

    void ChasePlayer(Vector2 direction)
    {
        transform.Translate(speed * Time.deltaTime * direction);
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
            yield return new WaitForSeconds(stateInfo.length);
        }
        Destroy(explosion);

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
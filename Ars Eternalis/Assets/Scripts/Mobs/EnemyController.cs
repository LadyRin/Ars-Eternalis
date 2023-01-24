using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Attacking
    public float timeBetweenAttacks;
    bool hasAlreadyAttacked;
    public GameObject projectile;
    public float health;

    //States
    public float attackRange;
    public bool playerInAttackRange = true;

    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(target.position, transform.position);
        playerInAttackRange = false;
        //if (!playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange) AttackPlayer();
    }

    /* private void ChasePlayer() {
        agent.SetDestination(target.position);
    }

    private void AttackPlayer() {
        agent.SetDestination(transform.position);
        transform.LookAt(target);
        if (!hasAlreadyAttacked) {
            // Destroy(rb);
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 40f, ForceMode.Impulse);
            rb.AddForce(transform.up * 20f, ForceMode.Impulse);

            hasAlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    } */

    private void ResetAttack()
    {
        hasAlreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

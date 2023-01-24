using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
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
    public bool playerInSightRange;
    public bool playerInAttackRange = true; 

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        playerInAttackRange = distance <= attackRange;
        if (!playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    private void ChasePlayer() {
        agent.SetDestination(target.position);
        transform.LookAt(target);
        if (!hasAlreadyAttacked) {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 32f, ForceMode.Impulse);

            hasAlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void AttackPlayer() {
        // agent.Stop()
    }

    private void ResetAttack() {
        hasAlreadyAttacked = false;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) DestroyEnemy();
    }

    private void DestroyEnemy() {
        
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

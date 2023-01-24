using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange; 
    // Attacking
    public float timeBetweenAttacks;
    bool hasAlreadyAttacked;

    //States
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange = true; 

    private  

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
    
    private void SearchWalkPoint() {
        // float randomZ = Random.Range()
    }

    private void ChasePlayer() {
        agent.SetDestination(target.position);
    }

    private void AttackPlayer() {

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

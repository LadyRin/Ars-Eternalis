using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour, IDamageable, IShootable
{
    Transform player;
    NavMeshAgent agent;
    Animator animator;
    Collider meleeHitbox;

    [SerializeField] bool isMelee;
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;
    [SerializeField] float attackDelayStart;
    [SerializeField] float attackDuration;
    [SerializeField] float attackDelayEnd;
    EnemyBaseState currentState;

    public EnemyBaseState trackingState;
    public EnemyBaseState meleeAttackState;
    public EnemyBaseState rangedAttackState;
    public EnemyBaseState travelingState;
    public EnemyBaseState deadState;

    void InitStates() {
        trackingState = new EnemyTrackingState(this);
        meleeAttackState = new EnemyMeleeAttackState(this);
        rangedAttackState = new EnemyRangedAttackState(this);
        travelingState = new EnemyTravelingState(this);
        deadState = new EnemyDeadState(this);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        InitStates();

        currentState = trackingState;
    }

    public void GetShot(Vector3 hitPoint, Collider hitCollider, float damage)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public EnemyBaseState CurrentState { get => currentState; set => currentState = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public Transform Player { get => player; set => player = value; }
    public bool IsMelee { get => isMelee; set => isMelee = value; }
    public Collider MeleeHitbox { get => meleeHitbox; set => meleeHitbox = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float AttackDelayStart { get => attackDelayStart; set => attackDelayStart = value; }
    public float AttackDuration { get => attackDuration; set => attackDuration = value; }
    public float AttackDelayEnd { get => attackDelayEnd; set => attackDelayEnd = value; }
    


   
}

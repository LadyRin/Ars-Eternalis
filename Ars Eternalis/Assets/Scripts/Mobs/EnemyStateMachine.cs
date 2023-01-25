using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour, IDamageable, IShootable
{
    Transform player;
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] Collider meleeHitbox;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject deathParticleSystem;
    [SerializeField] GameObject bloodParticleSystem;
    [SerializeField] bool isMelee;
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;
    [SerializeField] float attackDelayStart;
    [SerializeField] float attackDuration;
    [SerializeField] float attackDelayEnd;
    [SerializeField] float health;
    EnemyBaseState currentState;

    
    [HideInInspector] public EnemyBaseState decisionState;
    [HideInInspector] public EnemyBaseState meleeAttackState;
    [HideInInspector] public EnemyBaseState rangedAttackState;
    [HideInInspector] public EnemyBaseState travelingState;
    [HideInInspector] public EnemyBaseState deadState;

    void InitStates()
    {
        decisionState = new EnemyDecisionState(this);
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

        currentState = decisionState;
        currentState.EnterState();
    }

    void Update()
    {
        currentState.UpdateState();
        currentState.CheckSwitchStates();
        Debug.Log("Current state: " + currentState);
    }

    public void GetShot(Vector3 hitPoint, Collider hitCollider, float damage)
    {
        TakeDamage(damage);
        GameObject blood = Instantiate(bloodParticleSystem, hitPoint, Quaternion.identity);
        Destroy(blood, 2f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
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
    public float Health { get => health; set => health = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }
    public GameObject DeathParticleSystem { get => deathParticleSystem; set => deathParticleSystem = value; }
}

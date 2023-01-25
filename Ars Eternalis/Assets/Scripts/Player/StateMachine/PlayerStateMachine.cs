using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerStateMachine : MonoBehaviour
{
    // Current State
    PlayerBaseState currentState;
    public PlayerBaseState groundedState;
    public PlayerBaseState airborneState;
    public PlayerBaseState deadState;

    Vector2 inputMove;
    Vector2 inputLook;
    bool isJumping;
    bool isFiring;
    bool isAbility1;
    bool isAbility2;
    bool isAbility3;
    bool isAbility4;
    private bool isFreezeReloading;
    private float health;
    private bool ability3Usable = true;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airAcceleration;
    [SerializeField] float maxAirSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private HudManager hudManager;
    CinemachineVirtualCamera virtualCamera;
    Camera trueCamera;
    TimeMachine timeMachine;
    Rigidbody rb;

    void InitStates() {
        groundedState = new PlayerGroundedState(this);
        airborneState = new PlayerAirborneState(this); 
        deadState = new PlayerDeadState(this);
    }

    void Awake() {
        rb = GetComponent<Rigidbody>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        trueCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
        hudManager?.SetMaxHealth(maxHealth);
        hudManager?.SetHealth(health);
        timeMachine = FindObjectOfType<TimeMachine>();
        isFreezeReloading = false;
        
        InitStates();
    
        currentState = groundedState;
        currentState.EnterState();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckSwitchStates();
        currentState.UpdateState();
    }

    public void OnLook(InputAction.CallbackContext context) {
        inputLook = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        inputMove = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        isJumping = context.ReadValueAsButton();
    }

    public void OnFire(InputAction.CallbackContext context) {
        isFiring = context.ReadValueAsButton();
    }

    public void OnAbility1(InputAction.CallbackContext context) {
        isAbility1 = context.ReadValueAsButton();
    }

    public void OnAbility2(InputAction.CallbackContext context) {
        isAbility2 = context.ReadValueAsButton();
    }

    public void OnAbility3(InputAction.CallbackContext context) {
        isAbility3 = context.ReadValueAsButton();
    }

    public void OnAbility4(InputAction.CallbackContext context) {
        isAbility4 = context.ReadValueAsButton();
    }

    public new void StartCoroutine(IEnumerator enumerator)
    {
        
        base.StartCoroutine(enumerator);
    }

    public PlayerBaseState CurrentState { 
        get {
            return currentState;
        } 
        set {
            currentState = value;
        }
    }

    public bool IsFreezeReloading
    {
        get {
            return isFreezeReloading;
        }
        set {
            isFreezeReloading = value;
        }
    }
    
    public Vector2 InputMove {get {return inputMove;}}
    public Vector2 InputLook {get {return inputLook;}}

    public bool IsJumping {get {return isJumping;}}
    public bool IsFiring {get {return isFiring;}}
    public bool IsAbility1 {get {return isAbility1;}}
    public bool IsAbility2 {get {return isAbility2;}}
    public bool IsAbility3 {get {return isAbility3;}}
    public bool IsAbility3Usable {get {return ability3Usable;} set {ability3Usable = value;}}
    public bool IsAbility4 {get {return isAbility4;}}
    public float Health {get {return health;} set {health = value;}}
    public float MaxHealth {get {return maxHealth;}}
    
    public Rigidbody Rigidbody {get {return rb;}}
    public CinemachineVirtualCamera VirtualCamera {get {return virtualCamera;}}
    public Camera TrueCamera {get {return trueCamera;}}
    public TimeMachine TimeMachine {get {return timeMachine;}}

    public float JumpForce {get {return jumpForce;}}
    public float MoveSpeed {get {return moveSpeed;}}
    public float MouseSensitivity {get {return mouseSensitivity;}}
    public float AirAcceleration {get {return airAcceleration;}}
    public float MaxAirSpeed {get {return maxAirSpeed;}}
    public Weapon activeWeapon {get {return GetComponentInChildren<Weapon>();}}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMeleeHitbox"))
        {
            EnemyStateMachine enemy = other.GetComponentInParent<EnemyStateMachine>();
            if (enemy != null)
            {
                health -= enemy.AttackDamage;
                hudManager?.SetHealth(health);
                if (health <= 0)
                {
                    currentState = deadState;
                    currentState.EnterState();
                }
            }
        }
    }
}

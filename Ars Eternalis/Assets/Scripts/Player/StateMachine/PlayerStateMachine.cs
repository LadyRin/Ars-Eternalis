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
    private float health;
    private float lastFreezing = 0;
    private bool ability3Usable = true;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airAcceleration;
    [SerializeField] float maxAirSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private HudManager hudManager;
    [SerializeField] private float freezingDelay;
    CinemachineVirtualCamera mainCamera;
    Camera cam;
    TimeMachine timeMachine;
    Rigidbody rb;

    void InitStates() {
        groundedState = new PlayerGroundedState(this);
        airborneState = new PlayerAirborneState(this); 
        deadState = new PlayerDeadState(this);
    }

    void Awake() {
        rb = GetComponent<Rigidbody>();
        mainCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        cam = mainCamera.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
        hudManager?.SetMaxHealth(maxHealth);
        hudManager?.SetHealth(health);
        timeMachine = FindObjectOfType<TimeMachine>();
        
        InitStates();
    
        currentState = groundedState;
        currentState.EnterState();
        Debug.Log("Current State: " + currentState);
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

    public PlayerBaseState CurrentState { 
        get {
            return currentState;
        } 
        set {
            currentState = value;
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

    public float FreezingDelay {get { return FreezingDelay; }}
    public float LastFreezing
    {
        get { return LastFreezing; }
        set => LastFreezing = value;
    }
    
    public Rigidbody Rigidbody {get {return rb;}}
    public CinemachineVirtualCamera MainCamera {get {return mainCamera;}}
    public Camera TrueCamera {get {return cam;}}
    public TimeMachine TimeMachine {get {return timeMachine;}}

    public float JumpForce {get {return jumpForce;}}
    public float MoveSpeed {get {return moveSpeed;}}
    public float MouseSensitivity {get {return mouseSensitivity;}}
    public float AirAcceleration {get {return airAcceleration;}}
    public float MaxAirSpeed {get {return maxAirSpeed;}}
    public Weapon activeWeapon {get {return GetComponentInChildren<Weapon>();}}

}

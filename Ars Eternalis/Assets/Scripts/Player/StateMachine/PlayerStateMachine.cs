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
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airAcceleration;
    [SerializeField] float maxAirSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private HudManager hudManager;
    [SerializeField] private float freezingDelay;
    CinemachineVirtualCamera mainCamera;
    Rigidbody rb;

    void InitStates()
    {
        groundedState = new PlayerGroundedState(this);
        airborneState = new PlayerAirborneState(this);
        deadState = new PlayerDeadState(this);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
        //hudManager.SetMaxHealth(maxHealth);
        //hudManager.SetHealth(health);

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

    public void OnLook(InputAction.CallbackContext context)
    {
        inputLook = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            isJumping = context.ReadValueAsButton();
        else
            isJumping = false;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.ReadValueAsButton();
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        if (context.started)
            isAbility1 = context.ReadValueAsButton();
        else
            isAbility1 = false;
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        if (context.started)
            isAbility2 = context.ReadValueAsButton();
        else
            isAbility2 = false;
    }

    public void OnAbility3(InputAction.CallbackContext context)
    {
        if (context.started)
            isAbility3 = context.ReadValueAsButton();
        else
            isAbility3 = false;
    }

    public void OnAbility4(InputAction.CallbackContext context)
    {
        if (context.started)
            isAbility4 = context.ReadValueAsButton();
        else
            isAbility4 = false;
    }

    public PlayerBaseState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }

    public Vector2 InputMove { get { return inputMove; } }
    public Vector2 InputLook { get { return inputLook; } }
    
    public bool IsJumping {get {return isJumping;}}
    public bool IsFiring {get {return isFiring;}}
    
    public bool IsAbility1 {get { return isAbility1; }}
    public float FreezingDelay {get { return FreezingDelay; }}
    public float LastFreezing
    {
        get { return LastFreezing; }
        set => LastFreezing = value;
    }

    public Rigidbody Rigidbody { get { return rb; } }
    public CinemachineVirtualCamera MainCamera { get { return mainCamera; } }

    public float JumpForce { get { return jumpForce; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float MouseSensitivity { get { return mouseSensitivity; } }
    public float AirAcceleration { get { return airAcceleration; } }
    public float MaxAirSpeed { get { return maxAirSpeed; } }
    public Weapon activeWeapon { get { return GetComponentInChildren<Weapon>(); } }

}

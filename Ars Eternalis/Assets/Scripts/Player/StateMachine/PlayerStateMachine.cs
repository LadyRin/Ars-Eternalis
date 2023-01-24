using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airAcceleration;
    [SerializeField] float maxAirSpeed;
    [SerializeField] private float maxHealth;
    private float health;
    CinemachineVirtualCamera mainCamera;
    Rigidbody rb;

    void InitStates() {
        groundedState = new PlayerGroundedState(this);
        airborneState = new PlayerAirborneState(this);
        deadState = new PlayerDeadState(this);
    }

    void Awake() {
        rb = GetComponent<Rigidbody>();
        mainCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
        
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

    public Rigidbody Rigidbody {get {return rb;}}
    public CinemachineVirtualCamera MainCamera {get {return mainCamera;}}

    public float JumpForce {get {return jumpForce;}}
    public float MoveSpeed {get {return moveSpeed;}}
    public float MouseSensitivity {get {return mouseSensitivity;}}
    public float AirAcceleration {get {return airAcceleration;}}
    public float MaxAirSpeed {get {return maxAirSpeed;}}
    public Weapon activeWeapon {get {return GetComponentInChildren<Weapon>();}}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine currentContext) : base(currentContext) { }
    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (IsGrounded())
        {
            SwitchState(context.groundedState);
            Debug.Log("Switching to Grounded State");
        }
    }

    private void Move() {
        Vector2 inputMove = context.InputMove;
        float moveSpeed = context.MoveSpeed;
        Transform transform = context.transform;
        Rigidbody rb = context.Rigidbody;
        
        Vector2 velocity = inputMove * moveSpeed;
        Vector3 moveVelocity = transform.right * velocity.x + transform.forward * velocity.y;
        moveVelocity.y = rb.velocity.y;

        rb.velocity = moveVelocity;
    }

    private void Look()
    {
        Vector2 inputLook = context.InputLook;
        Vector2 lookVelocity = inputLook * context.MouseSensitivity;
        Transform transform = context.transform;
        var mainCamera = context.MainCamera;

        float horizontalLook = lookVelocity.x * Time.deltaTime;
        float verticalLook = lookVelocity.y * Time.deltaTime;

        transform.Rotate(Vector3.up, horizontalLook);

        float cameraRotation = mainCamera.transform.localEulerAngles.x;
        cameraRotation -= verticalLook;
        if (cameraRotation > 180)
        {
            cameraRotation -= 360;
        }

        cameraRotation = Mathf.Clamp(cameraRotation, -90, 90);
        mainCamera.transform.localEulerAngles = new Vector3(cameraRotation, 0, 0);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(context.transform.position, Vector3.down, 1.1f);
    }

}

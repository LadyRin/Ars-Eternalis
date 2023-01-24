using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAliveState : PlayerBaseState
{
    public PlayerAliveState(PlayerStateMachine currentContext) : base(currentContext) { }
    public override void UpdateState()
    {
        Move();
        Look();
        Fire();
    }

    private void Fire()
    {
        if (context.IsFiring)
        {
            context.activeWeapon.TryUse();
        }
    }

    private void Move()
    {
        Vector2 inputMove = context.InputMove;
        float moveSpeed = context.MoveSpeed;

        Vector3 moveVelocity = new Vector3(inputMove.x, 0, inputMove.y) * moveSpeed;
        moveVelocity = context.transform.TransformDirection(moveVelocity);
        moveVelocity.y = context.Rigidbody.velocity.y;

        context.Rigidbody.velocity = moveVelocity;
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

}

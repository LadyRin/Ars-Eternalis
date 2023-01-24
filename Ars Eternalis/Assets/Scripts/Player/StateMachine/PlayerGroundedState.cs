using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerAliveState
{
    public PlayerGroundedState(PlayerStateMachine currentContext) : base(currentContext) { }

    public override void CheckSwitchStates()
    {
        if (context.IsJumping)
        {
            handleJump();
            SwitchState(context.airborneState);
        }
    }

    protected override void Move()
    {
        Vector2 inputMove = context.InputMove;
        float moveSpeed = context.MoveSpeed;

        Vector3 moveVelocity = new Vector3(inputMove.x, 0, inputMove.y) * moveSpeed;
        moveVelocity = context.transform.TransformDirection(moveVelocity);
        moveVelocity.y = context.Rigidbody.velocity.y;

        context.Rigidbody.velocity = moveVelocity;
    }

    public override void EnterState()
    {
        Debug.Log("Switching to Grounded State");
    }

    public override void ExitState()
    {
        //
    }

    private void handleJump()
    {
        context.Rigidbody.AddForce(Vector3.up * context.JumpForce, ForceMode.Impulse);
    }
}

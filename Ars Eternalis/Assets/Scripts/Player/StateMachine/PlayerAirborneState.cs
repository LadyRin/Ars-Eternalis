using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerAliveState
{
    public PlayerAirborneState(PlayerStateMachine currentContext) : base(currentContext) { }

    LineRenderer lineRenderer;
    public override void CheckSwitchStates()
    {
        if (IsGrounded())
        {
            SwitchState(context.groundedState);
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
        lineRenderer = context.GetComponent<LineRenderer>();
    }

    public override void ExitState()
    {
        //
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(context.transform.position, Vector3.down, 1.1f);
    }

}

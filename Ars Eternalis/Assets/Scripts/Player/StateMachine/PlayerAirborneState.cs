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
            Debug.Log("Switching to Grounded State");
        }
    }

    protected override void Move()
    {
        Vector2 inputMove = context.InputMove;
        float airAcceleration = context.AirAcceleration;
        float maxAirSpeed = context.MaxAirSpeed;
        Transform transform = context.transform;
        Rigidbody rigidbody = context.Rigidbody;

        Vector2 acceleration = inputMove * airAcceleration;
        acceleration = transform.TransformDirection(new Vector3(acceleration.x, 0, acceleration.y));
        Vector2 currentVelocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

        Vector2 i = currentVelocity.normalized == Vector2.zero ? Vector2.right : currentVelocity.normalized;
        Vector2 j = Vector2.Perpendicular(i);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + new Vector3(i.x, 0, i.y) * 5);

        float accelerationAlongI = Vector2.Dot(acceleration, i);
        float accelerationAlongJ = Vector2.Dot(acceleration, j);

        float velocityAlongI = Vector2.Dot(currentVelocity, i);
        float velocityAlongJ = Vector2.Dot(currentVelocity, j);

        if (accelerationAlongI > 0 && velocityAlongI > maxAirSpeed)
            accelerationAlongI = 0;

        if (accelerationAlongJ > 0 && velocityAlongJ > maxAirSpeed)
            accelerationAlongJ = 0;

        Vector2 accelerationInLocalSpace = accelerationAlongI * i + accelerationAlongJ * j;
        Vector3 accelerationInWorldSpace = transform.TransformDirection(new Vector3(accelerationInLocalSpace.x, 0, accelerationInLocalSpace.y));

        rigidbody.AddForce(accelerationInWorldSpace, ForceMode.Acceleration);
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

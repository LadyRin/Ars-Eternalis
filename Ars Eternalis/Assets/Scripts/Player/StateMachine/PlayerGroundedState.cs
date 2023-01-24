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

    public override void EnterState()
    {
        //
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

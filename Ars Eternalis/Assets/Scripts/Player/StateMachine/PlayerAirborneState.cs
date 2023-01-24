using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerAliveState
{
    public PlayerAirborneState(PlayerStateMachine currentContext) : base(currentContext) { }


    public override void CheckSwitchStates()
    {
        if (IsGrounded())
        {
            SwitchState(context.groundedState);
            Debug.Log("Switching to Grounded State");
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

    private bool IsGrounded()
    {
        return Physics.Raycast(context.transform.position, Vector3.down, 1.1f);
    }

}

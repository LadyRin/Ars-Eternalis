using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionState : EnemyBaseState
{
    public EnemyDecisionState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState()
    {
        if (context.IsMelee && Vector3.Distance(context.transform.position, context.Player.position) <= context.AttackRange)
        {
            SwitchState(context.meleeAttackState);
            return;
        }

        SwitchState(context.travelingState);
    }
    public override void UpdateState()
    {
        SwitchState(context.travelingState);
    }
    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (context.Health <= 0)
        {
            SwitchState(context.deadState);
        }
    }
}

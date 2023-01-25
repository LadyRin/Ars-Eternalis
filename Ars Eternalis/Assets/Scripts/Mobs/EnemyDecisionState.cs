using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionState : EnemyBaseState
{
    public EnemyDecisionState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState()
    {
        if(context.IsMelee)
        {
            if(Vector3.Distance(context.transform.position, context.Player.position) < context.AttackRange)
            {
                SwitchState(context.meleeAttackState);
            }
            else
            {
                SwitchState(context.travelingState);
            }
        }
        else
        {
            if (Vector3.Distance(context.transform.position, context.Player.position) > context.AttackRange)
            {
                SwitchState(context.rangedAttackState);
            }
            else
            {
                SwitchState(context.travelingState);
            }
        }
    }
    public override void UpdateState() { }
    public override void ExitState() { }    
}

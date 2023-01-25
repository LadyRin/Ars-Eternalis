using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
    public override void CheckSwitchStates() { }
}

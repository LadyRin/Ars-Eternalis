using UnityEngine;

public abstract class EnemyBaseState
{
    protected EnemyStateMachine context;

    public EnemyBaseState(EnemyStateMachine currentContext) {
        context = currentContext;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();
    
    protected void SwitchState(EnemyBaseState newState){
        // Exit current state 

        ExitState();
        newState.EnterState();
        context.CurrentState = newState;
    }
}

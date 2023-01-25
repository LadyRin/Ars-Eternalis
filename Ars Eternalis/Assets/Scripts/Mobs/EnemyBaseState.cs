using UnityEngine;

public abstract class EnemyBaseState: MonoBehaviour
{
    protected EnemyStateMachine context;

    public EnemyBaseState(EnemyStateMachine currentContext) {
        context = currentContext;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public void CheckSwitchStates() {
        if(context.Health <= 0)
        {
            SwitchState(context.deadState);
        }
    }
    
    protected void SwitchState(EnemyBaseState newState){
        // Exit current state 

        ExitState();
        newState.EnterState();
        context.CurrentState = newState;
    }
}

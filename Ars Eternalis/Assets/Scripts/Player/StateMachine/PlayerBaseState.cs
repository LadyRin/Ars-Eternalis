using UnityEngine;

public abstract class PlayerBaseState 
{
    protected PlayerStateMachine context;

    public PlayerBaseState(PlayerStateMachine currentContext) {
        context = currentContext;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    void UpdateStates(){}
    
    protected void SwitchState(PlayerBaseState newState){
        // Exit current state 

        ExitState();
        newState.EnterState();
        context.CurrentState = newState;
    }

    protected void SetSuperState(){}

    protected void SetSubState(){}
}

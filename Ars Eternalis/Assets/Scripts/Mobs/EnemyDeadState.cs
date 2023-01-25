using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState()
    {
        context.Animator.SetTrigger("Death");
        context.Agent.isStopped = true;
        Death();
    }
    public override void UpdateState() { }
    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        // Do nothing
    }

    private void Death()
    {
        var ps = Object.Instantiate(context.DeathParticleSystem, context.transform.position, context.transform.rotation);
        ps.GetComponent<ParticleSystem>().Play();
        Object.Destroy(ps, 10f);
        Object.Destroy(context.gameObject, 2f);
    }
}

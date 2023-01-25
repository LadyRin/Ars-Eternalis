using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState() {
        context.Animator.SetTrigger("Die");
        Death();
     }
    public override void UpdateState() { }
    public override void ExitState() { }

    public new void CheckSwitchStates() { 
        // Do nothing
    }


    private void Death()
    {
        var ps = Instantiate(context.DeathParticleSystem, context.transform.position, context.transform.rotation);
        ps.GetComponent<ParticleSystem>().Play();
        Destroy(ps, 5f);
        Destroy(context.gameObject, 2f);
    }
}

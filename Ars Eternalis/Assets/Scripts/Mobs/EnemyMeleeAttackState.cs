using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyBaseState
{
    private Animator animator;

    public EnemyMeleeAttackState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState()
    {
        animator = context.Animator;
        animator.SetTrigger("Attack");
        context.StartCoroutine(Attack());
    }
    public override void UpdateState() { }
    public override void ExitState() { }

    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(context.AttackDelayStart);
        context.MeleeHitbox.enabled = true;
        yield return new WaitForSeconds(context.AttackDuration);
        context.MeleeHitbox.enabled = false;
        yield return new WaitForSeconds(context.AttackDelayEnd);
        SwitchState(context.decisionState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackState : EnemyBaseState
{
    private Animator animator;
    private GameObject projectile;
    public EnemyRangedAttackState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState() { 
        animator = context.Animator;
        animator.SetTrigger("Fire");
        context.StartCoroutine(Fire());
    }
    public override void UpdateState() { }
    public override void ExitState() { }

    private IEnumerator Fire() {
        yield return new WaitForSeconds(context.AttackDelayStart);
        projectile = Instantiate(context.Projectile, context.transform.position, context.transform.rotation);
        projectile.GetComponent<Projectile>().SetTarget(context.Player);
        yield return new WaitForSeconds(context.AttackDelayEnd);
        SwitchState(context.decisionState);
    }
}

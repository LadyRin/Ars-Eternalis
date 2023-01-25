using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTravelingState : EnemyBaseState
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private Animator animator;

    public EnemyTravelingState(EnemyStateMachine currentContext) : base(currentContext) { }
    public override void EnterState()
    {
        animator = context.Animator;
        agent = context.Agent;
        agent.isStopped = false;
        animator.SetBool("IsMoving", true);

        if (context.IsMelee)
        {
            destination = context.Player.position;
        }
        else
        {
            //find a random point in the navmesh away from the player
            Vector3 randomDirection = Random.insideUnitSphere * 10;
            randomDirection += context.Player.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
            destination = hit.position;
        }
        agent.SetDestination(destination);
    }
    public override void UpdateState()
    {
        destination = context.Player.position;
        agent.SetDestination(destination);
    }
    public override void ExitState()
    {
        animator.SetBool("IsMoving", false);
        agent.isStopped = true;
    }
    public override void CheckSwitchStates()
    {
        if(context.Health <= 0)
        {
            SwitchState(context.deadState);
        }
        
        if (Vector3.Distance(context.transform.position, destination) <= context.AttackRange)
        {
            SwitchState(context.meleeAttackState);
        }
    }
}

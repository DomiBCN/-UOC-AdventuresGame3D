using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IZombieState
{
    ZombieAI zombie;

    public PatrolState(ZombieAI zombie)
    {
        this.zombie = zombie;
    }

    public void GoToAlertState()
    {
        zombie.agent.isStopped = true;
        zombie.currentState = zombie.alertState;
    }

    public void GoToAttackState() { }

    public void GoToPatrolState()
    {
        throw new System.NotImplementedException();
    }

    public void Impact()
    {
        GoToAlertState();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerExit(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        //if (col.gameObject.tag == "Player")
        //{
        //    GoToAlertState();
        //}
    }

    public void UpdateState()
    {
        if (zombie.target != null)
            zombie.agent.SetDestination(zombie.target.position);
        if (!zombie.agent.pathPending)
        {
            if (zombie.agent.remainingDistance > zombie.agent.stoppingDistance)
            {
                //Debug.Log(zombie.agent.desiredVelocity);
                zombie.character.Move(zombie.agent.desiredVelocity, false, false, false);
            }
            else
            {
                zombie.character.Move(Vector3.zero, false, false, false);
                zombie.RespawnWayPoint();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IZombieState
{
    ZombieAI zombie;
    float actualTimeBetweenAttacks = 0;

    public AttackState(ZombieAI zombie)
    {
        this.zombie = zombie;
    }

    public void GoToAlertState()
    {
        zombie.currentState = zombie.alertState;
    }

    public void GoToAttackState() { }

    public void GoToPatrolState() { }

    public void Impact() { }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerExit(Collider col)
    {
        GoToAlertState();
    }

    public void OnTriggerStay(Collider col) { }

    public void UpdateState()
    {
        actualTimeBetweenAttacks += Time.deltaTime;

        zombie.agent.SetDestination(zombie.player.transform.position);

        if (!zombie.agent.pathPending)
        {
            if (zombie.agent.remainingDistance > zombie.agent.stoppingDistance)
            {
                GoToAlertState();
            }
            else
            {
                if (actualTimeBetweenAttacks > zombie.timeBetweenAttacks)
                {
                    zombie.character.Move(Vector3.zero, false, false, true);
                }
            }
        }
    }
}

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

    public void GoToPatrolState()
    {
        zombie.agent.isStopped = false;
        zombie.target = zombie.waypoint;
        zombie.currentState = zombie.patrolState;
    }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerExit(Collider col)
    {
        GoToAlertState();
    }

    public void OnTriggerStay(Collider col) { }

    public void UpdateState()
    {
        if (!DayNightManager.instance.isGameOver)
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
                        if (!zombie.zombieAudioSource.isPlaying)
                        {
                            zombie.zombieAudioSource.clip = zombie.zombieAttackClips[Random.Range(0, zombie.zombieAttackClips.Count - 1)];
                            zombie.zombieAudioSource.Play();
                        }
                    }
                }
            }
        }
        else
        {
            GoToPatrolState();
        }
    }
}

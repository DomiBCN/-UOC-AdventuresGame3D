using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IZombieState
{
    ZombieAI zombie;
    float currentRotationTime = 0;
    bool playerFound;

    public AlertState(ZombieAI zombie)
    {
        this.zombie = zombie;
    }

    public void GoToAlertState() { }

    public void GoToAttackState()
    {
        currentRotationTime = 0;
        playerFound = false;
        zombie.currentState = zombie.attackState;
    }

    public void GoToPatrolState()
    {
        currentRotationTime = 0;
        playerFound = false;
        zombie.agent.isStopped = false;
        zombie.target = zombie.waypoint;
        zombie.currentState = zombie.patrolState;
    }
    
    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerExit(Collider col) { }

    public void OnTriggerStay(Collider col) { }

    public void UpdateState()
    {
        if (!zombie.zombieAudioSource.isPlaying)
        {
            zombie.zombieAudioSource.clip = zombie.zombieIdleClips[Random.Range(0, zombie.zombieIdleClips.Count - 1)];
        }

        if (!playerFound) FaceTarget();//zombie.character.Move(zombie.transform.right, false, false, false);

        if (currentRotationTime > zombie.rotationTime && !playerFound)
        {
            GoToPatrolState();
        }
        else
        {
            if (zombie.fieldOfView.canSeeTarget)
            {
                zombie.agent.isStopped = false;
                playerFound = true;
                zombie.agent.SetDestination(zombie.fieldOfView.lastKnownPosition);

                if (!zombie.agent.pathPending)
                {
                    if (zombie.agent.remainingDistance > zombie.agent.stoppingDistance)
                    {
                        zombie.character.Move(zombie.agent.desiredVelocity, false, false, false);
                    }
                    else
                    {
                        zombie.agent.isStopped = true;
                        if(zombie.fieldOfView.canSeeTarget)
                        {
                            GoToAttackState();
                        }
                        else
                        {
                            GoToPatrolState();
                        }
                    }
                }
            }
            else
            {
                playerFound = false;
            }
            currentRotationTime += Time.deltaTime;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (zombie.player.transform.position - zombie.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

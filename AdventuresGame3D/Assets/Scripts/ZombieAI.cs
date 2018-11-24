using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ZombieAI : MonoBehaviour {

    [HideInInspector] public GameObject player;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IZombieState currentState;
    [HideInInspector] public ZombieSpawner mySpawner;
     public Transform waypoint;
    [HideInInspector] public FieldOfView fieldOfView;

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public ThirdPersonCharacter character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for
    public float rotationTime = 3.0f;
    public float timeBetweenAttacks = 1.0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
       
        character = GetComponent<ThirdPersonCharacter>();
        fieldOfView = GetComponent<FieldOfView>();
        currentState = patrolState;
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updatePosition = true;
        
    }


    private void Update()
    {
        currentState.UpdateState();
        //if (target != null)
        //    //agent.SetDestination(target.position);
        //    character.Move(transform.right, false, false, false);
        //if (!agent.pathPending)
        //{
        //    if (agent.remainingDistance > agent.stoppingDistance)
        //    {
        //        //Debug.Log(agent.desiredVelocity);
        //        //character.Move(agent.desiredVelocity, false, false, false);
        //        //character.Move(transform.right, false, false, false);
        //    }
        //    else
        //        character.Move(Vector3.zero, false, false, false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void RespawnWayPoint()
    {
        mySpawner.TeleportAtRandomPosition(waypoint.gameObject);
    }
}

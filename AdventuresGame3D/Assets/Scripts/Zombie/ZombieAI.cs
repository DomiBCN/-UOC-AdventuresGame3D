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
    [HideInInspector] public ZombieFieldOfView fieldOfView;
    
    public Animator zombieAnimator;
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public ThirdPersonCharacter character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for
    public float rotationTime = 3.0f;
    public float timeBetweenAttacks = 1.0f;

    [Header("Audio")]
    public AudioSource zombieAudioSource;
    public List<AudioClip> zombieIdleClips;
    public List<AudioClip> zombieAttackClips;
    public List<AudioClip> zombieImpactClips;
    public AudioClip zombieDieClip;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
       
        character = GetComponent<ThirdPersonCharacter>();
        fieldOfView = GetComponent<ZombieFieldOfView>();
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

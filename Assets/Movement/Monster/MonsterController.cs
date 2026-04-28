using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MonsterController : GenericController
{
    [Header("State Machine")]
    public StateMachine<MonsterController> stateMachine;

    [Header("States")]
    public MonsterStandingState standingState;
    public MonsterFollowState followState;

    [Header("Player Inputs")]
    public InputActionReference move;
    public InputActionReference attack;

    [Header("Following")]
    public Transform owner;
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        // Nav Mesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<MonsterController>();

        // Grounded
        followState = new MonsterFollowState(this, stateMachine);
        standingState = new MonsterStandingState(this, stateMachine);

        stateMachine.Initialize(followState);


        cameraTransform = Camera.main.transform;

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void GetOwner(Transform transform)
    {
        owner = transform;
    }
}


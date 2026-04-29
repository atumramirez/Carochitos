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
    public MonsterSwapingState swapState;

    [Header("Player Inputs")]
    public InputAction move;
    public InputAction attack;
    public InputAction swap;

    [Header("Following")]
    public Transform owner;
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        

        // Nav Mesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<MonsterController>();

        // Grounded
        followState = new MonsterFollowState(this, stateMachine);
        standingState = new MonsterStandingState(this, stateMachine);
        swapState = new MonsterSwapingState(this, stateMachine);

        stateMachine.Initialize(followState);

        // PLayer Input
        playerInput = GetComponent<PlayerInput>();

        move = playerInput.actions.FindActionMap("Monster").FindAction("Move");
        swap = playerInput.actions.FindActionMap("Monster").FindAction("Switch");



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


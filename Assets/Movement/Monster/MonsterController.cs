using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Skill;

public class MonsterController : GenericController
{
    [Header("State Machine")]
    public StateMachine<MonsterController> stateMachine;

    [Header("States")]
    public MonsterStandingState standingState;
    public MonsterFollowState followState;
    public MonsterSwapingState swapState;

    public MonsterAttackState attackState;

    [Header("Player Inputs")]
    public InputAction move;
    public InputAction attack;
    public InputAction swap;

    public InputAction ability1;
    public InputAction ability2;
    public InputAction ability3;
    public InputAction ability4;

    [Header("Following")]
    public Transform owner;
    public NavMeshAgent navMeshAgent;

    [Header("Carochito")]
    public CarochitoHandler handler;

    [Header("Skills")]
    float _cooldownTime;
    float _activeTime;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Handler
        handler = GetComponent<CarochitoHandler>();
        
        // Nav Mesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<MonsterController>();

        // Grounded
        followState = new MonsterFollowState(this, stateMachine);
        standingState = new MonsterStandingState(this, stateMachine);
        swapState = new MonsterSwapingState(this, stateMachine);

        attackState = new MonsterAttackState(this, stateMachine);

        stateMachine.Initialize(followState);

        // PLayer Input 
        playerInput = GetComponent<PlayerInput>();

        move = playerInput.actions.FindActionMap("Monster").FindAction("Move");
        swap = playerInput.actions.FindActionMap("Monster").FindAction("Switch");

        // Attacks s
        ability1 = playerInput.actions.FindActionMap("Monster").FindAction("Ability1");
        ability2 = playerInput.actions.FindActionMap("Monster").FindAction("Ability2");
        ability3 = playerInput.actions.FindActionMap("Monster").FindAction("Ability3");
        ability4 = playerInput.actions.FindActionMap("Monster").FindAction("Ability4");

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

    public void Attack(Skill skill)
    {
        foreach (Skill skills in handler.carochito.Skills)
        {
            if (skill == skills)
            {
                if (skills.State == SkillsState.Ready)
                {
                    skills.Base.Activate(gameObject);
                    skills.State = SkillsState.Active;
                    _activeTime = skills.Base.ActiveTime;
                }
            }
        }

    }
}


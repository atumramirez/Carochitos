using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class TrainerController : GenericController
{
    [Header("State Machine")]
    public StateMachine<TrainerController> stateMachine;

    [Header("States")]
    public StandingState standing;

    public JumpingState jumping;
    public FallingState airTime;
    public LandingState landing;

    public CrouchingState crouching;
    public RollState diveRoll;

    public SprintState sprinting;

    /*
    
    
    
    public CaptureState capture;
    
    public AiState aiState;
    public SummonState summonState;
    public SwapToMonsterState changeCharacterState;
    public MenuState menuState;
    */

    [Header("Player Inputs")]
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference crouch;
    public InputActionReference sprint;

    [Header("CharacterHandler")]
    public CharacterHandler characterHandler;

    [Header("Menu")]
    public PlayerMenu menuHolder;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        menuHolder = FindFirstObjectByType<PlayerMenu>();

        cameraTransform = Camera.main.transform;

        stateMachine = new StateMachine<TrainerController>();
        
        standing = new StandingState(this, stateMachine);
        jumping = new JumpingState(this, stateMachine);
        airTime = new FallingState(this, stateMachine);
        landing = new LandingState(this, stateMachine);
        crouching = new CrouchingState(this, stateMachine);
        sprinting = new SprintState(this, stateMachine);
        diveRoll = new RollState(this, stateMachine);

        /*
        
        capture = new CaptureState(this, movementSM);
        
        aiState = new AiState(this, movementSM);
        summonState = new SummonState(this, movementSM);
        changeCharacterState = new SwapToMonsterState(this, movementSM);
        menuState = new MenuState(this, movementSM);
        */

        stateMachine.Initialize(standing);

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

    public void Summon()
    {
        characterHandler.Summon();
    }

    public void Swap()
    {
        characterHandler.SwapCharacter();
    }
}

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
    public FallingState falling;
    public LandingState landing;

    public CrouchingState crouching;
    public RollState roll;

    public SprintState sprinting;

    public CaptureState capturing;

    /*
    
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

    public InputActionReference capture;

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
        
        // Grounded
        standing = new StandingState(this, stateMachine);
        crouching = new CrouchingState(this, stateMachine);
        sprinting = new SprintState(this, stateMachine);
        roll = new RollState(this, stateMachine);

        // Jumping and Air Time
        jumping = new JumpingState(this, stateMachine);
        falling = new FallingState(this, stateMachine);
        landing = new LandingState(this, stateMachine);

        // Actions
        capturing = new CaptureState(this, stateMachine);


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

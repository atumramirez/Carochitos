using System;
using Unity.Cinemachine;
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

    // Aiming
    public ThrowingState throwing;

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
    public InputActionReference throwin;

    [Header("CharacterHandler")]
    public CharacterHandler characterHandler;

    [Header("Menu")]
    public PlayerMenu menuHolder;

    [Header("Cameras")]
    public CinemachineCamera[] cameras;

    public CinemachineCamera thirdPersonCam;
    public CinemachineCamera combatCam;

    public CinemachineCamera startCamera;
    private CinemachineCamera currentCamera;

    private Transform combatCameraTransform; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        menuHolder = FindFirstObjectByType<PlayerMenu>();

        // Cameras

        currentCamera = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCamera)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }

        cameraTransform = currentCamera.transform;

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

        // Throwing
        throwing = new ThrowingState(this, stateMachine);


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

    public void SwitchCamera(CinemachineCamera newCam)
    {
        currentCamera = newCam;

        currentCamera.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCamera)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}

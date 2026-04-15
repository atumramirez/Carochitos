using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class TrainerController : GenericController
{
    [Header("States")]
    public StandingState standing;
    public JumpingState jumping;
    public CrouchingState crouching;
    public LandingState landing;
    public SprintState sprinting;
    public CaptureState capture;
    public DiveRollState diveRoll;
    public AiState aiState;
    public SummonState summonState;
    public SwapToMonsterState changeCharacterState;

    [Header("CharacterHandler")]
    public CharacterHandler characterHandler;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();

        // Estados 
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        crouching = new CrouchingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        capture = new CaptureState(this, movementSM);
        diveRoll = new DiveRollState(this, movementSM);
        aiState = new AiState(this, movementSM);
        summonState = new SummonState(this, movementSM);
        changeCharacterState = new SwapToMonsterState(this, movementSM);

        movementSM.Initialize(standing);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();
        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
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

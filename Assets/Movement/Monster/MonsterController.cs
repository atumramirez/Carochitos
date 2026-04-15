using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterController : GenericController
{
    public MonsterStandingState standingState;
    public MonsterAiState monsterAiState;
    public SwapToTrainerState swapToTrainerState;


    public CharacterHandler characterHandler;
    private void Start()
    {
        characterHandler = FindFirstObjectByType<CharacterHandler>();

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();

        standingState = new MonsterStandingState(this, movementSM);
        monsterAiState = new MonsterAiState(this, movementSM);
        swapToTrainerState = new SwapToTrainerState(this, movementSM);


        movementSM.Initialize(monsterAiState);

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
}

using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


/*
public class MonsterController : GenericController
{
    public MonsterStandingState standingState;
    public MonsterAiState monsterAiState;
    public SwapToTrainerState swapToTrainerState;
    public MonsterLockOnState lockOnState;


    public CharacterHandler characterHandler;

    private void Start()
    {
        characterHandler = FindFirstObjectByType<CharacterHandler>();

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = Camera.main.transform;

        stateMachine = new StateMachine();

        standingState = new MonsterStandingState(this, stateMachine);
        monsterAiState = new MonsterAiState(this, stateMachine);
        swapToTrainerState = new SwapToTrainerState(this, stateMachine);
        lockOnState = new MonsterLockOnState(this, stateMachine);


        stateMachine.Initialize(monsterAiState);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        stateMachine.currentState.HandleInput();
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
}
*/

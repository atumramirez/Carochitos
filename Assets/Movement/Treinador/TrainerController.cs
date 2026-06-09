using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class TrainerController : GenericController
{
    #region Variaveis
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
    public SwapingState swaping;
    public ThrowingState throwing;
    public StopState stop;

    [Header("Player Input")]
    public InputManager inputManager;

    [Header("Cameras")]
    public CameraHandler cameraHandler;

    private Transform combatCameraTransform;

    [Header("Player Data")]
    public Inventory inventory;

    [Header("Bola de Berlim")]
    public ItemBase bolaDeBeerlim;

    #endregion

    #region Methods
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();


        // Player Input
        inputManager.playerInput = GetComponent<PlayerInput>();
        inputManager.playerInput.actions.FindActionMap("Trainer").Enable();
        inputManager.playerInput.actions.FindActionMap("Monster").Disable();

        // Nav Mesh
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;

        // Cameras
        cameraHandler.Initialize();
        cameraHandler.LookAt(transform);

        cameraTransform = cameraHandler.CurrentCamera.transform;

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
        swaping = new SwapingState(this, stateMachine);
        
        // Throwing
        throwing = new ThrowingState(this, stateMachine);

        // Summoning and Dismissing
        summoning = new SummonState(this, stateMachine);  
        dismissing = new DismissState(this, stateMachine);

        // SFollowing
        following = new FollowingState(this, stateMachine);

        // Stop
        stop = new StopState(this, stateMachine);

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
    #endregion

    #region Thrwoing
    [Header("Throwing")]

    public GameObject objectToThrow;
    public Transform throwPoint;
    public float throwForce = 10f;

    public void Throw()
    {
        if (inventory.CanRemoveItem(bolaDeBeerlim) == true)
        {
            Debug.Log("Lançar Bola de Berlim!");

            GameObject obj = Instantiate(objectToThrow, throwPoint.position, Quaternion.identity);

            if (obj.TryGetComponent<Rigidbody>(out var rb))
            {
                Transform cam = Camera.main.transform;

                Vector3 forward = cam.forward;

                float upwardForce = 0.5f;

                Vector3 throwDirection = (forward + Vector3.up * upwardForce).normalized;

                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }

            inventory.RemoveItem(bolaDeBeerlim, 1);
        }
    }

    public class ThrowingState : State<TrainerController>
    {
        bool grounded;

        float playerSpeed;
        float gravityValue;

        private float airTime;

        Vector3 currentVelocity;
        Vector3 cVelocity;

        public ThrowingState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
        {
            character = _character;
            stateMachine = _stateMachine;
        }

        public override void Enter()
        {
            base.Enter();

            character.cameraHandler.SwitchCamera(character.cameraHandler.combatCam);

            character.inputManager.throwin.action.started += PressAim;
            character.inputManager.capture.action.started += Throw;
        }

        private void PressAim(InputAction.CallbackContext context)
        {
            Debug.Log("The Throw Button was pressed");
            stateMachine.ChangeState(character.standing);
        }

        private void Throw(InputAction.CallbackContext context)
        {
            Debug.Log("The Throw Button was pressed");
            character.Throw();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            input = character.inputManager.move.action.ReadValue<Vector2>();

            // Flatten camera directions (ignore vertical tilt)
            Vector3 camForward = character.cameraTransform.forward;
            Vector3 camRight = character.cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            // Camera-relative movement
            velocity = camRight * input.x + camForward * input.y;

            // Animator
            character.animator.SetFloat
            (
                "speed",
                input.magnitude,
                character.speedDampTime,
                Time.deltaTime
            );
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            grounded = character.controller.isGrounded;

            // Gravity
            gravityVelocity.y += character.gravityValue * Time.deltaTime;

            if (grounded && gravityVelocity.y < 0)
            {
                gravityVelocity.y = -2f;
            }

            // Smooth movement
            currentVelocity = Vector3.SmoothDamp(
                currentVelocity,
                velocity,
                ref cVelocity,
                character.velocityDampTime
            );

            character.controller.Move
            (
                character.playerSpeed * Time.deltaTime * currentVelocity +
                gravityVelocity * Time.deltaTime
            );

            // -------- ROTATION LOGIC --------

            // Face camera forward while aiming

            Vector3 aimDirection = character.cameraTransform.forward;
            aimDirection.y = 0f;

            if (aimDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(aimDirection);

                character.transform.rotation = Quaternion.Slerp
                (
                    character.transform.rotation,
                    targetRotation,
                    character.rotationDampTime
                );
            }
        }

        public override void Exit()
        {
            base.Exit();

            character.inputManager.throwin.action.started -= PressAim;
            character.inputManager.jump.action.started -= Throw;
        }
    }
    #endregion

    [Header("Monster")]
    public GameObject monster;

    [Header("Summoning")]
    public SummonState summoning;
    public DismissState dismissing;

    public bool isMonsterSpawned = false;
    public Transform monsterSpawnPoint;
    public GameObject monsterPrefab;
    public void Summon()
    {
        monster = Instantiate(monsterPrefab, monsterSpawnPoint.position, monsterSpawnPoint.rotation);
        monster.GetComponent<MonsterController>().GetOwner(this.transform);

        isMonsterSpawned = true;
    }

    public void Dismiss()
    {
        Destroy(monster);
        monster = null; // Destruir o Objecto, sem depois deixar ele como Null pode causar problemas. 

        isMonsterSpawned = false;
    }

    [Header("Swaping")]
    public FollowingState following;
    public NavMeshAgent navMeshAgent;
    public bool isControllingMonster = false;

    public HudHandler hudHandler;

    public void SwapToMonster()
    {
        inputManager.ControlMonster();

        navMeshAgent.enabled = true;
        monster.GetComponent<MonsterController>().navMeshAgent.enabled = false;

        stateMachine.ChangeState(following);
        monster.GetComponent<MonsterController>().stateMachine.ChangeState(monster.GetComponent<MonsterController>().standingState);

        cameraHandler.LookAt(monster.transform);


        hudHandler.OpenMonterHud();

        // Adicionar a modificação do Rig da Camera em Runtime

        isControllingMonster = true;
    }

    public void SwapToTrainer()
    {
        inputManager.ControlTrainer();

        cameraHandler.LookAt(transform);

        navMeshAgent.enabled = false;
        monster.GetComponent<MonsterController>().navMeshAgent.enabled = true;

        stateMachine.ChangeState(standing);
        monster.GetComponent<MonsterController>().stateMachine.ChangeState(monster.GetComponent<MonsterController>().followState);

        hudHandler.OpenTrainerHud();

        isControllingMonster = false;
    }

    #region Interact
    [Header("Interact")]
    [SerializeField] private Transform _interactArea;
    [SerializeField] private float _interactAreaSize = 1f;
    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(_interactArea.position, _interactAreaSize);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.OnInteract();
                break;
            }
        }
    }
    #endregion

    #region Menu
    [Header("Menu")]
    public PlayerMenu playerMenu;
    public void OpenMenu()
    {
        playerMenu.ActivateMenu();
    }

    [Header("Change Current Carochito")]
    public Party party;

    public void NextCarochito()
    {
        party.NextCarochito();
    }

    public void PreviousCarochito()
    {
        party.Previous();
    }
    #endregion
}

using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.TextCore.Text;

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
    public MenuState menu;

    [Header("Player Input")]
    public InputManager inputManager;

    [Header("Cameras")]
    public GameSceneManager sceneManager;
    public CameraHandler cameraHandler;
    public PlayerInfo playerInfo;

    private Transform combatCameraTransform;

    [Header("Player Data")]
    public Party party;
    public Inventory inventory;

    [Header("Noise")]
    public NoiseArea noiseArea;

    [Header("SpawnZone")]
    public Transform spawnZone;

    #endregion

    #region Methods
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Player Input
        inputManager.playerInput.actions.FindActionMap("Trainer").Enable();
        inputManager.playerInput.actions.FindActionMap("Monster").Disable();

        // Nav Mesh
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;

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
        menu = new MenuState(this, stateMachine);

        stateMachine.Initialize(stop);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;

        RefreshCamera();
        EndCapture();
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

    #region Capture

    [Header("Hammer")]
    public GameObject CaptureArea;
    public GameObject Hammer;

    public ItemBase _disk;

    public void StartCapture()
    {
        CaptureArea.SetActive(true);
        Hammer.SetActive(true);
    }

    public void EndCapture()
    {
        CaptureArea.SetActive(false);
        Hammer.SetActive(false);
    }

    public void Capture(CarochitoBattler carochito)
    {
        carochito.Capture();


        if (inventory.CanRemoveItem(_disk) == true)
        {
            inventory.RemoveItem(_disk, 1);

            carochito.Capture();
        }
    }
    #endregion

    #region Thrwoing
    [Header("Throwing")]
    public Transform throwPoint;
    public float throwForce = 10f;

    public void Throw()
    {
        if (inventory.CanRemoveItem(inventory.currentBerliner.Item) == true)
        {
            if (inventory.currentBerliner.Item is Berliner berliner)
            {
                animator.SetTrigger("throw");

                GameObject berlinerModel = Instantiate(berliner._model, throwPoint.position, Quaternion.identity);
                berlinerModel.GetComponent<BerlinerBall>().SetFlavour(berliner._flavour);

                if (berlinerModel.TryGetComponent<Rigidbody>(out var rb))
                {
                    Transform cam = Camera.main.transform;
                    Vector3 forward = cam.forward;
                    float upwardForce = 0.5f;
                    Vector3 throwDirection = (forward + Vector3.up * upwardForce).normalized;
                    rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
                }

                inventory.RemoveItem(berliner, 1);

                inventory.berlinerMenu.RefreshMenu(inventory);
            }
        }
    }

    public void NextBerliner()
    {
        inventory.NextBerliner();
        inventory.berlinerMenu.RefreshMenu(inventory);
    }

    public void PreviousBerliner()
    {
        inventory.PreviousBerliner();
        inventory.berlinerMenu.RefreshMenu(inventory);
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

            character.animator.SetTrigger("aim");

            character.inventory.berlinerMenu.OpenBerliner();

            character.inputManager.throwin.action.started += PressAim;
            character.inputManager.capture.action.started += Throw;

            character.inputManager.next.action.started += PressNext;
            character.inputManager.previous.action.started += PressPrevious;
        }

        private void PressPrevious(InputAction.CallbackContext context)
        {
            character.PreviousBerliner();
        }

        private void PressNext(InputAction.CallbackContext context)
        {
            character.NextBerliner();
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

            Vector3 camForward = character.cameraTransform.forward;
            Vector3 camRight = character.cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            velocity = camRight * input.x + camForward * input.y;

            character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            grounded = character.controller.isGrounded;

            gravityVelocity.y += character.gravityValue * Time.deltaTime;

            if (grounded && gravityVelocity.y < 0)
            {
                gravityVelocity.y = -2f;
            }

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

            character.cameraHandler.SwitchCamera(character.cameraHandler.thirdPersonCam);

            character.animator.SetTrigger("move");

            character.inventory.berlinerMenu.OpenMenu();

            character.inputManager.throwin.action.started -= PressAim;
            character.inputManager.capture.action.started -= Throw;

            character.inputManager.next.action.started -= PressNext;
            character.inputManager.previous.action.started -= PressPrevious;
        }
    }
    #endregion

    #region Summoning
    [Header("Monster")]
    public GameObject monster;

    [Header("Summoning")]
    public SummonState summoning;
    public DismissState dismissing;

    public bool isMonsterSpawned = false;
    public Transform monsterSpawnPoint;
    public void Summon()
    {
        if (party.partyCarochitos.Count > 0)
        {
            if (party.currentCarochito.CurrentHealth > 0 || isMonsterSpawned == false)
            {
                monster = Instantiate(party.currentCarochito.Base.Model, monsterSpawnPoint.position, monsterSpawnPoint.rotation);

                monster.GetComponent<CarochitoBattler>().SetUp(party.currentCarochito, false, false, this);
                
                isMonsterSpawned = true;
            }
        }
    }

    public void Dismiss()
    {
        Destroy(monster);
        monster = null;

        isMonsterSpawned = false;
    }
    #endregion

    #region Swaping

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

        //hudHandler.OpenMonterHud();

        sceneManager._bookMenu.pageHolders[1].OpenPage(1);

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

        sceneManager._bookMenu.pageHolders[1].OpenPage(0);

        isControllingMonster = false;
    }

    #endregion

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

    #region Camera
    public void RefreshCamera()
    {
        cameraHandler.Initialize(sceneManager._currentEnviroment);
        cameraHandler.LookAt(headPivot);
        cameraTransform = cameraHandler.CurrentCamera.transform;
    }
    #endregion

    #region Menu
    [Header("Menu")]
    public PlayerMenu playerMenu;
    public bool menuOpen = false;
    public void OpenMenu()
    {
        if (menuOpen == false)
        {
            sceneManager._bookMenu.OpenBook(3);
            menuOpen = true;
        }
        else
        {
            stateMachine.ChangeState(standing);
            sceneManager._bookMenu.OpenBook(1);
            menuOpen = false;
        }     
    }

    public void NextCarochito()
    {
        party.NextCarochito();
    }

    public void PreviousCarochito()
    {
        party.PreviousCarochito();
    }
    #endregion
}

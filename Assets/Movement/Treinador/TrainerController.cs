using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
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
    public SwapingState swaping;



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

    [Header("Menu")]
    public PlayerMenu menuHolder;

    [Header("Cameras")]
    public CinemachineCamera[] cameras;

    public CinemachineCamera thirdPersonCam;
    public CinemachineCamera combatCam;

    public CinemachineCamera startCamera;
    private CinemachineCamera currentCamera;

    private Transform combatCameraTransform;

    [Header("Player Data")]
    public Inventory inventory;

    [Header("Bola de Berlim")]
    public ItemBase bolaDeBeerlim;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();

        menuHolder = FindFirstObjectByType<PlayerMenu>();

        // Nav Mesh
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;

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
        swaping = new SwapingState(this, stateMachine);
        

        // Throwing
        throwing = new ThrowingState(this, stateMachine);

        // Summoning and Dismissing
        summoning = new SummonState(this, stateMachine);  
        dismissing = new DismissState(this, stateMachine);

        // Swaping characters
        following = new FollowingState(this, stateMachine);

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

    [Header("Monster")]
    public GameObject monster;

    [Header("Summoning")]
    public InputActionReference summon;
    public InputActionReference dismiss;

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

    public void SwapToMonster()
    {
        navMeshAgent.enabled = true;

        stateMachine.ChangeState(following);
        monster.GetComponent<MonsterController>().stateMachine.ChangeState(monster.GetComponent<MonsterController>().standingState);

        isControllingMonster = true;
    }
    public void SwapToTrainer()
    {
        navMeshAgent.enabled = false;

        stateMachine.ChangeState(standing);
        monster.GetComponent<MonsterController>().stateMachine.ChangeState(monster.GetComponent<MonsterController>().followState);

        isControllingMonster = false;
    }

}

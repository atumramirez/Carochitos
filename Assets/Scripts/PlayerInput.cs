using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController _playerController;
    PlayerInteract _playerInteract;
    Vector3 moveVector;

    CharacterSwap _characterSwap;
    public PlayerMenu _playerMenu;
    public PlayerSummoner _playerSummoner;
    public CarochitoParty _c;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerInteract = GetComponent<PlayerInteract>();

        _characterSwap = FindAnyObjectByType<CharacterSwap>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");

        _playerController.AddMoveVectorInput(moveVector);

        if(Input.GetMouseButtonDown(1))
        {
            _playerInteract.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _characterSwap.SwapToNext();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerMenu.ActivateMenu();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            _playerSummoner.ActivateSummon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _c.Previous();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _c.NextCarochito();
        }
    }
}

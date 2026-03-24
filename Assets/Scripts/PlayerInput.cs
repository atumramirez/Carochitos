using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController _playerController;
    PlayerInteract _playerInteract;
    Vector3 moveVector;

    CharacterSwap _characterSwap;

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
    }
}

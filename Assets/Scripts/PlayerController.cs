using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 moveVectorInput;

    Vector3 moveDirection;
    Vector3 rotationDirection;

    private readonly float speed = 10f;
    private readonly float rotationSpeed = 5f;

    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;
    [SerializeField] private Camera _camera;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponentInChildren<PlayerAnimator>();
        _camera = FindFirstObjectByType<Camera>();
        
    }

    public void AddMoveVectorInput(Vector3 moveVector)
    { 
        moveVectorInput = moveVector; 
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        if (_playerAnimator != null)
        {
            HandleAnimation();
        }
    }

    private void HandleAnimation()
    {
        _playerAnimator.motion = moveVectorInput.magnitude;
    }

    private void HandleRotation()
    {
        if (moveDirection.magnitude > 0f)
        {
            rotationDirection = moveDirection;
        }

        if(rotationDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }
    }

    private void HandleMovement()
    {
        moveDirection = _camera.transform.forward * moveVectorInput.z;
        moveDirection += _camera.transform.right * moveVectorInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        Vector3 moveVelocity = moveDirection * speed;
        moveVelocity += Physics.gravity;

        _rigidbody.linearVelocity = moveVelocity;
    }
}

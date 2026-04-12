using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 moveVectorInput;

    Vector3 moveDirection;
    Vector3 rotationDirection;

    private readonly float speed = 8f;
    private readonly float rotationSpeed = 7f;

    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private bool isGrounded;

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
        CheckGround();

        HandleMovement();
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

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

        if (rotationDirection != Vector3.zero)
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
        moveVelocity.y = _rigidbody.linearVelocity.y; // preserve vertical velocity

        _rigidbody.linearVelocity = moveVelocity;
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void Jump()
    {
        if (!isGrounded) return;

        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.y = 0f;
        _rigidbody.linearVelocity = velocity;

        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

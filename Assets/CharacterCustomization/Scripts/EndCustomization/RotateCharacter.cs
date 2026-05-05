using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCharacter : MonoBehaviour
{
    [Header("Object")]
    public GameObject Object;

    [Header("Rotation Settings")]
    public float rotationSpeed = 120f;
    public float smoothTime = 0.1f;

    private float targetYRotation;
    private float currentVelocity;
    private float initialYRotation;

    [Header("Input References")]
    public InputActionReference rotate;
    public InputActionReference reset;


    private float rotateInput;

    private void Awake()
    {
        rotate.action.performed += ctx => rotateInput = ctx.ReadValue<float>() * -1;
        rotate.action.canceled += ctx => rotateInput = 0f;

        reset.action.performed += ctx => ResetRotation();
    }

    private void OnEnable()
    {
        rotate.action.Enable();
        reset.action.Enable();
    }

    private void OnDisable()
    {
        rotate.action.Disable();
        reset.action.Disable();
    }

    private void Start()
    {
        initialYRotation = 180f;
        targetYRotation = initialYRotation;
    }

    private void Update()
    {
        // Update target rotation based on input
        targetYRotation += rotateInput * rotationSpeed * Time.deltaTime;

        // Smoothly interpolate toward target rotation
        float currentY = Object.transform.eulerAngles.y;
        float smoothY = Mathf.SmoothDampAngle(currentY, targetYRotation, ref currentVelocity, smoothTime);

        Object.transform.rotation = Quaternion.Euler(0f, smoothY, 0f);
    }

    private void ResetRotation()
    {
        targetYRotation = initialYRotation;
    }
}

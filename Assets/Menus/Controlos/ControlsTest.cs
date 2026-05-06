using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsTest : MonoBehaviour
{
    public InputActionReference jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        jump.action.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Saltar");
    }
}

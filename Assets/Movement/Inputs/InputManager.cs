using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;

    [Header("Trainer")]
    // Movement
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference crouch;
    public InputActionReference sprint;

    // Capturing 
    public InputActionReference capture;
    public InputActionReference throwin;

    // Interact
    public InputActionReference interact;

    // Change Carochito
    public InputActionReference next;
    public InputActionReference previous;


    // Summoning and Dismissing Monster
    public InputActionReference summon;
    public InputActionReference dismiss;

    // Menu
    public InputActionReference menu;

    [Header("Monster")]
    public InputActionReference monsterMove;
    public InputActionReference swap;

    // Use Dash
    public InputActionReference dash;

    // Use Abilities
    public InputActionReference ability1;
    public InputActionReference ability2;
    public InputActionReference ability3;
    public InputActionReference ability4;

    [Header("Input Maps")]
    private InputActionMap trainerMap;
    private InputActionMap monsterMap;

    private void Start()
    {
        trainerMap = playerInput.actions.FindActionMap("Trainer");
        monsterMap = playerInput.actions.FindActionMap("Monster");

        ControlTrainer();
    }

    public void ControlTrainer()
    {
        trainerMap.Enable();
        monsterMap.Disable();
    }

    public void ControlMonster() 
    {
        monsterMap.Enable();
        trainerMap.Disable();
    }
}

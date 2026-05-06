using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsRebinding : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionsAsset;
    public PlayerInput playerInput;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private InputAction currentAction;

    // Start rebinding by action name
    public void StartRebinding(string actionName)
    {
        Debug.Log($"Rebinding started for {actionName}");

        currentAction = actionsAsset.FindAction(actionName);

        if (currentAction == null)
        {
            Debug.LogError($"Action '{actionName}' not found.");
            return;
        }

        playerInput.currentActionMap.Disable();

        rebindingOperation = currentAction.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        Debug.Log("Rebinding ended");

        rebindingOperation.Dispose();

        CheckForConflicts(currentAction);

        playerInput.currentActionMap.Enable();
    }

    // Reset ALL bindings to default
    public void ResetBindings()
    {
        Debug.Log("Resetting all bindings");

        foreach (var map in actionsAsset.actionMaps)
        {
            foreach (var action in map.actions)
            {
                action.RemoveAllBindingOverrides();
            }
        }
    }

    // Conflict detection
    private void CheckForConflicts(InputAction changedAction)
    {
        string newBindingPath = changedAction.bindings
            .FirstOrDefault(b => b.isPartOfComposite == false).effectivePath;

        foreach (var map in actionsAsset.actionMaps)
        {
            foreach (var action in map.actions)
            {
                if (action == changedAction) continue;

                for (int i = 0; i < action.bindings.Count; i++)
                {
                    var binding = action.bindings[i];

                    if (binding.effectivePath == newBindingPath)
                    {
                        Debug.Log($"Conflict detected: {action.name} had same key. Removing it.");

                        action.ApplyBindingOverride(i, "");
                    }
                }
            }
        }
    }
}


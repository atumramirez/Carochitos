using System.Collections.Generic;
using System.Reflection;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    private readonly Dictionary<string, BaseActionNode> _nodeLookUp = new();
    public Dictionary<string, BaseActionNode> NodeList
    {
        get { return _nodeLookUp; }
    }
    private BaseActionNode _currentNode;
    public BaseActionNode CurrentNode
    {
        get { return _currentNode; }
    }

    private RuntimeDialogueGraph _currentGraph;

    public TrainerController trainerController;

    [Header("Manager")]
    public BookMenu bookMenu;
    public DialogueManager dialogueManager;
    public GameSceneManager gameSceneManager;
    public CameraHandler cameraHandler;
    public PlayerInfo playerInfo;
    public Party party;
    public Inventory inventory;

    [Header("Character")]
    public GameObject chara;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Open Graph
    public void OpenGraph(RuntimeDialogueGraph graph)
    {
        _currentGraph = graph;
        _nodeLookUp.Clear();

        if (trainerController.isControllingMonster == true)
        {
            trainerController.SwapToTrainer();
        }

        bookMenu.OpenBook(4);

        foreach (var node in graph.AllNodes)
        {
            _nodeLookUp[node.NodeID] = node;
        }

        StartAction();

        if (trainerController.stop != null)
        {
            trainerController.stateMachine.ChangeState(trainerController.stop);
        }
    }

    // Close Graph
    public void CloseGraph()
    {
        _currentNode = null;
        _nodeLookUp.Clear();
        _currentGraph = null;

        bookMenu.OpenBook(1);

        Debug.Log("Graph Closed");

        trainerController.stateMachine.ChangeState(trainerController.standing);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start Action
    public void StartAction()
    {
        if (_currentGraph == null || string.IsNullOrEmpty(_currentGraph.EntryNodeID))
        {
            CloseGraph();
            return;
        }

        if (!_nodeLookUp.TryGetValue(_currentGraph.EntryNodeID, out _currentNode))
        {
            CloseGraph();
            return;
        }

        _currentNode.Perform();
    }

    // End Action
    public void EndAction(string overrideNodeID = null)
    {
        if (_currentNode == null)
        {
            CloseGraph();
            return;
        }

        // End current node
        _currentNode.End();

        // Decide next node
        string nextID = overrideNodeID ?? _currentNode.NextNodeID;

        if (_currentNode is ConditionAction processVariableAction)
        {
            bool firstValue = GetVariableByName(processVariableAction.Variable);

            if (firstValue == true)
            {
               nextID = processVariableAction.nextTrueId;
            }
            else
            {
                nextID = processVariableAction.nextFalseId;
            }
        }

        if (_currentNode is SwitchAction swicthVariableAction)
        {
            int firstValue = GetVariableByNameInt(swicthVariableAction.Variable);

            if (firstValue == swicthVariableAction.Value)
            {
                nextID = swicthVariableAction.nextTrueId;
            }
            else
            {
                nextID = swicthVariableAction.nextFalseId;
            }
        }

        if (string.IsNullOrEmpty(nextID))
        {
            CloseGraph();
            return;
        }

        // Get next node
        if (!_nodeLookUp.TryGetValue(nextID, out _currentNode))
        {
            CloseGraph();
            return;
        }

        // Start next node
        _currentNode.Perform();
    }

    public bool GetVariableByName(string variableName)
    {
        FieldInfo field = playerInfo.GetType().GetField(variableName);

        if (field != null)
        {
            Debug.Log("It's True");
            return (bool)field.GetValue(playerInfo);
        }

        Debug.Log("It's False");
        return false;
    }

    public int GetVariableByNameInt(string variableName)
    {
        FieldInfo field = playerInfo.GetType().GetField(variableName);

        if (field != null)
        {
            Debug.Log("It's True");
            return (int)field.GetValue(playerInfo);
        }

        Debug.Log("It's False");
        return 0;
    }


    public void ChangeVariable()
    {
        if (_currentNode is FlagAction processVariableAction)
        {
            FieldInfo field = playerInfo.GetType().GetField(processVariableAction.Variable);

            if (field != null && field.FieldType == typeof(bool))
            {
                field.SetValue(playerInfo, processVariableAction.Value);
            }

            EndAction();
        }

        if (_currentNode is IntAction intAction)
        {
            FieldInfo field = playerInfo.GetType().GetField(intAction.Variable);

            if (field != null && field.FieldType == typeof(int))
            {
                field.SetValue(playerInfo, intAction.Value);
            }

            EndAction();
        }

    }

    /*
    public void SpawnCarochito()
    {
        if (_currentNode is SpawnAction processVariableAction)
        {
            Instantiate(
            processVariableAction.Carochito,
            processVariableAction.Position.position,
            processVariableAction.Position.rotation
            );

            EndAction();
        }
    }
    */

    public void GiveCarochito()
    {
        if (_currentNode is CarochitoAction processVariableAction)
        { 
            Debug.Log("Dar");
            Carochito carochito = new(processVariableAction.Carochito, processVariableAction.Level);
            party.AddCarochito(carochito);

            EndAction();
        }
    }

    public void RemoveCharacter()
    {
        if (_currentNode is RemoveAction removeAction)
        {
            Debug.Log("Crazy");
            string objectName = removeAction.Character;
            
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (!scene.isLoaded)
                    continue;

                foreach (GameObject rootObject in scene.GetRootGameObjects())
                {
                    Transform found = FindChildRecursive(rootObject.transform, objectName);

                    if (found != null)
                    {
                        Debug.Log("Removido");
                        Destroy(found.gameObject);
                    }

                }
            }

            EndAction();
        }
    }

    public void TelerportCharacter()
    {
        if (_currentNode is PositionAction removeAction)
        {
            string objectName = removeAction.Object;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (!scene.isLoaded)
                    continue;

                foreach (GameObject rootObject in scene.GetRootGameObjects())
                {
                    Transform found = FindChildRecursive(rootObject.transform, objectName);

                    if (found != null)
                    {
                        Debug.Log("Teleport");
                        found.gameObject.transform.SetPositionAndRotation(removeAction.Position, removeAction.Rotation);
                    }

                }
            }

            EndAction();
        }

        Debug.Log("queijo");
    }

    public void FindCamera()
    {
        if (_currentNode is CameraAction cameraAction)
        {
            Debug.Log("Crazy");
            string objectName = cameraAction.Camera;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (!scene.isLoaded)
                    continue;

                foreach (GameObject rootObject in scene.GetRootGameObjects())
                {
                    Transform found = FindChildRecursive(rootObject.transform, objectName);

                    if (found != null)
                    {
                        cameraHandler.SwitchToCamera(found.GetComponent<CinemachineCamera>());
                    }
                }
            }

            EndAction();
        }
    }

    public void NormalCamera()
    {
        if (_currentNode is NormalCameraAction cameraAction)
        {
            cameraHandler.SwitchCamera(cameraHandler._sceneCamera);
            EndAction();
        }
    }

    private static Transform FindChildRecursive(Transform parent, string name)
    {
        if (parent.name == name)
            return parent;

        foreach (Transform child in parent)
        {
            Transform result = FindChildRecursive(child, name);

            if (result != null)
                return result;
        }

        return null;
    }


    public void StartBattle()
    {
        if (_currentNode is SpawnAction cameraAction)
        {
            string objectName = cameraAction.Arena;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (!scene.isLoaded)
                    continue;

                foreach (GameObject rootObject in scene.GetRootGameObjects())
                {
                    Transform found = FindChildRecursive(rootObject.transform, objectName);

                    if (found != null)
                    {
                        found.GetComponent<BattleArena>().StartBattle();
                    }
                }
            }

            EndAction();
        }
    }
}


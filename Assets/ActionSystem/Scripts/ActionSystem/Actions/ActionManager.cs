using System.Collections.Generic;
using UnityEngine;

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
    public DialogueManager dialogueManager;
    public GameSceneManager gameSceneManager;

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
        trainerController.stateMachine.ChangeState(trainerController.stop);

        _currentGraph = graph;
        _nodeLookUp.Clear();

        foreach (var node in graph.AllNodes)
        {
            _nodeLookUp[node.NodeID] = node;
        }

        StartAction();

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Close Graph
    public void CloseGraph()
    {
        _currentNode = null;
        _nodeLookUp.Clear();
        _currentGraph = null;

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
}


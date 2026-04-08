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

        foreach (var node in graph.AllNodes)
        {
            _nodeLookUp[node.NodeID] = node;
        }

        StartAction();
    }

    // Close Graph
    public void CloseGraph()
    {
        _currentNode = null;
        _nodeLookUp.Clear();
        _currentGraph = null;

        Debug.Log("Graph Closed");
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

        // 1. End current node
        _currentNode.End();

        // 2. Decide next node
        string nextID = overrideNodeID ?? _currentNode.NextNodeID;

        if (string.IsNullOrEmpty(nextID))
        {
            CloseGraph();
            return;
        }

        // 3. Get next node
        if (!_nodeLookUp.TryGetValue(nextID, out _currentNode))
        {
            CloseGraph();
            return;
        }

        // 4. Start next node
        _currentNode.Perform();
    }
}


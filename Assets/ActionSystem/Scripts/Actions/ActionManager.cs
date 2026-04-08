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

    public void StartGraph(RuntimeDialogueGraph RuntimeGraph)
    {
        foreach (var node in RuntimeGraph.AllNodes)
        {
            _nodeLookUp[node.NodeID] = node;
        }

        StartAction(RuntimeGraph);
    }

    public void StartAction(RuntimeDialogueGraph RuntimeGraph)
    {
        if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeID))
        {
            _currentNode = _nodeLookUp[RuntimeGraph.EntryNodeID];
            _currentNode.Perform();
        }
        else
        {
            EndGraph();
        }
    }

    public void EndAction()
    {
        if (!_nodeLookUp.ContainsKey(_currentNode.NextNodeID))
        {
            EndGraph();
            return;
        }

        NextAction(_currentNode.NextNodeID);
    }

    public void NextAction(string nodeID)
    {
        if (!string.IsNullOrEmpty(nodeID))
        {
            _currentNode = _nodeLookUp[nodeID];
            _currentNode.Perform();
        }
        else
        {
            EndGraph();
        }
    }

    public void EndGraph()
    {
        _currentNode = null;
        Debug.Log("Graph Ended");
    }
}

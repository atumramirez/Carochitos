using System.Collections.Generic;
using NUnit.Framework.Interfaces;
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

    public RuntimeDialogueGraph RuntimeGraph;

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

    public void StartGraph()
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

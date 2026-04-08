using UnityEditor.AssetImporters;
using Unity.GraphToolkit.Editor;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[ScriptedImporter(1, DialogueGraph.AssetExtension)]
public class DialogueGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DialogueGraph editorGraph = GraphDatabase.LoadGraphForImporter<DialogueGraph>(ctx.assetPath);
        RuntimeDialogueGraph runtimeGraph = ScriptableObject.CreateInstance<RuntimeDialogueGraph>();
        
        var nodeIDMap = new Dictionary<INode, string>();

        foreach (var node in editorGraph.GetNodes()) 
        {
            nodeIDMap[node] = Guid.NewGuid().ToString();
        }

        var startNode = editorGraph.GetNodes().OfType<StartNode>().FirstOrDefault();

        if (startNode != null)
        {
            var entryPort = startNode.GetOutputPorts().FirstOrDefault()?.firstConnectedPort;

            if (entryPort != null)
            {
                runtimeGraph.EntryNodeID = nodeIDMap[entryPort.GetNode()];
            }
        }

        foreach (var INode in editorGraph.GetNodes())
        {
            if (INode is StartNode || INode is EndNode) continue;

            if (INode is DialogueNode dialogueNode)
            {
                var runtimeNode = new SpeakAction { NodeID = nodeIDMap[INode] };
                ProcessDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }
            
            else if (INode is ChoiceNode choiceNode)
            {
                var runtimeNode = new QuestionAction { NodeID = nodeIDMap[INode] };
                ProcessChoiceNode(choiceNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }
        }

        // Attach the new runtime data to the asset itselft, this let us drag and drop the graph in the inspector
        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);
    }

    private void ProcessDialogueNode(DialogueNode node, SpeakAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessChoiceNode(ChoiceNode node, QuestionAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        var choiceOutPorts = node.GetOutputPorts().Where(p => p.name.StartsWith("Choice "));

        foreach(var outputPort in choiceOutPorts)
        {
            var index = outputPort.name.Substring("Choice ".Length);
            var textPort = node.GetInputPortByName($"Choice Text {index}");

            var choiceData = new QuestionAction.ChoiceData
            {
                ChoiceText = GetPortValue<string>(textPort),
                DestinationNodeID = outputPort.firstConnectedPort != null ? nodeIDMap[outputPort.firstConnectedPort.GetNode()] : null
            };
            

            runtimeNode.Choices.Add(choiceData);
        }
    }

    private T GetPortValue<T>(IPort port)
    {
        if (port == null) return default;

        // Check if the node is connected to blackboard variable
        if (port.isConnected)
        {
            if (port.firstConnectedPort.GetNode() is IVariableNode variableNode)
            {
                variableNode.variable.TryGetDefaultValue(out T value);
                return value;
            }
        }

        // If it's not connected to any balckboard variable, it will return what is typed manually
        port.TryGetValue(out T fallbackValue);
        return fallbackValue;
    }
}

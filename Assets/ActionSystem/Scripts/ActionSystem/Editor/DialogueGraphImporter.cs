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

            else if (INode is ChangeBackgroundNode changeBackgroundNode)
            {
                var runtimeNode = new ChangeBackgroundAction { NodeID = nodeIDMap[INode] };
                ProcessBackgroundNode(changeBackgroundNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }


            else if (INode is RemoveSpriteNode RemoveSpriteNode)
            {
                var runtimeNode = new RemoveSpriteAction { NodeID = nodeIDMap[INode] };
                ProcessRemoveSpriteNode(RemoveSpriteNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }

            else if (INode is FadeNode FadeNode)
            {
                var runtimeNode = new FadeAction { NodeID = nodeIDMap[INode] };
                ProcessFadeNode(FadeNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }

            else if (INode is WaitNode WaitNode)
            {
                var runtimeNode = new WaitAction { NodeID = nodeIDMap[INode] };
                ProcessWaitNode(WaitNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }

            else if (INode is ConditionNode ConditionNode)
            {
                var runtimeNode = new ConditionAction { NodeID = nodeIDMap[INode] };
                ProcessConditionNode(ConditionNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }

            else if (INode is TeleportNode TeleportNode)
            {
                var runtimeNode = new TeleportAction { NodeID = nodeIDMap[INode] };
                ProcessTeleportNode(TeleportNode, runtimeNode, nodeIDMap);
                runtimeGraph.AllNodes.Add(runtimeNode);
            }
        }

        // Attach the new runtime data to the asset itselft, this let us drag and drop the graph in the inspector
        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);
    }

    private void ProcessDialogueNode(DialogueNode node, SpeakAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        //  runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));

        // Dialogue
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        // Character
        runtimeNode.CharacterSprite = GetPortValue<CharacterSprite>(node.GetInputPortByName("Character"));
        runtimeNode.Emotion = GetPortValue<Emotion>(node.GetInputPortByName("Emotion"));

        //  Side
        runtimeNode.Side = GetPortValue<Side>(node.GetInputPortByName("Side"));

        // Audio
        runtimeNode.VoiceActing = GetPortValue<AudioClip>(node.GetInputPortByName("Voice Acting"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessChoiceNode(ChoiceNode node, QuestionAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        //runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));

        // Dialogue
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        // Character
        runtimeNode.CharacterSprite = GetPortValue<CharacterSprite>(node.GetInputPortByName("Character"));
        runtimeNode.Emotion = GetPortValue<Emotion>(node.GetInputPortByName("Emotion"));

        //  Side
        runtimeNode.Side = GetPortValue<Side>(node.GetInputPortByName("Side"));

        // Voice Acting
        runtimeNode.VoiceActing = GetPortValue<AudioClip>(node.GetInputPortByName("Voice Acting"));

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

    private void ProcessRemoveSpriteNode(RemoveSpriteNode node, RemoveSpriteAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.Side = GetPortValue<Side>(node.GetInputPortByName("Side"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessBackgroundNode(ChangeBackgroundNode node, ChangeBackgroundAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.Background = GetPortValue<Sprite>(node.GetInputPortByName("Background"));
        runtimeNode.FadeIn = GetPortValue<int>(node.GetInputPortByName("Fade In"));
        runtimeNode.FadeOut = GetPortValue<int>(node.GetInputPortByName("Fade Out"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessFadeNode(FadeNode node, FadeAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.Fade = GetPortValue<Fade>(node.GetInputPortByName("Fade"));
        runtimeNode.Duration = GetPortValue<int>(node.GetInputPortByName("Duration"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessWaitNode(WaitNode node, WaitAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.Duration = GetPortValue<int>(node.GetInputPortByName("Duration"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessConditionNode(ConditionNode node, ConditionAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.Variable = GetPortValue<bool>(node.GetInputPortByName("Variable"));
        runtimeNode.Condition = GetPortValue<Condition>(node.GetInputPortByName("Condition"));
        runtimeNode.Value = GetPortValue<bool>(node.GetInputPortByName("Value"));

        var nextNodePort = node.GetOutputPortByName("False")?.firstConnectedPort;

        switch (runtimeNode.Condition)
        {
            case Condition.Equal:

                if (runtimeNode.Variable == runtimeNode.Value)
                {
                    nextNodePort = node.GetOutputPortByName("True")?.firstConnectedPort;
                }
                else
                {
                    nextNodePort = node.GetOutputPortByName("False")?.firstConnectedPort;
                }
                break;

            case Condition.NotEqual:

                if (runtimeNode.Variable != runtimeNode.Value)
                {
                    nextNodePort = node.GetOutputPortByName("False")?.firstConnectedPort;
                }
                else
                {
                    nextNodePort = node.GetOutputPortByName("True")?.firstConnectedPort;
                }
                break;
        }

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private void ProcessTeleportNode(TeleportNode node, TeleportAction runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        // Dialogue
        runtimeNode.Scene = GetPortValue<string>(node.GetInputPortByName("Scene"));
        runtimeNode.WayPoint = GetPortValue<int>(node.GetInputPortByName("WayPoint"));

        var nextNodePort = node.GetOutputPortByName("Out")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    #region GetPortValue
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
    #endregion
}

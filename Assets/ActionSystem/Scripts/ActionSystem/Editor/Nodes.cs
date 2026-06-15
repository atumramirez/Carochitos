using System;
using JetBrains.Annotations;
using Unity.Cinemachine;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
public class StartNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddOutputPort("Out").Build();
    }
}

[Serializable]
public class EndNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
    }
}

[Serializable]
public class DialogueNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        // context.AddInputPort<string>("Speaker").Build();
        // context.AddInputPort<Sprite>("Character Sprite").Build();
        
        // Dialogue
        context.AddInputPort<string>("Dialogue").Build();

        // Character 
        context.AddInputPort<CharacterSprite>("Character").Build();
        context.AddInputPort<Emotion>("Emotion").Build();

        // Side
        context.AddInputPort<Side>("Side").Build();

        // Audio
        context.AddInputPort<AudioClip>("Voice Acting").Build();
    }
}

[Serializable]
public class ChoiceNode : Node
{
    const string optionID = "portCount";
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();

        // Dialogue
        context.AddInputPort<string>("Dialogue").Build();

        // Character
        context.AddInputPort<CharacterSprite>("Character").Build();
        context.AddInputPort<Emotion>("Emotion").Build();

        // Side
        context.AddInputPort<Side>("Side").Build();

        // Audio
        context.AddInputPort<AudioClip>("Voice Acting").Build();

        // Options
        var option = GetNodeOptionByName(optionID);
        option.TryGetValue(out int portCount);

        for (int i = 0; i < portCount; i++)
        {
            context.AddInputPort<string>($"Choice Text {i}").Build();
            context.AddOutputPort($"Choice {i}").Build();
        }
    }

    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        context.AddOption<int>(optionID).WithDefaultValue(2).Delayed();
    }
}

[Serializable]
public class RemoveSpriteNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<Side>("Side").Build();
    }
}

[Serializable]
public class ChangeBackgroundNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<Sprite>("Background").Build();
        context.AddInputPort<int>("Fade In").Build();
        context.AddInputPort<int>("Fade Out").Build();
    }
}

#region Fade
[Serializable]
public class FadeNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<Fade>("Fade").Build();
        context.AddInputPort<int>("Duration").Build();
    }
}
#endregion

#region Wait
[Serializable]
public class WaitNode: Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<int>("Duration").Build();
    }
}
#endregion

#region Condition
[Serializable]
public class ConditionNode: Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();

        context.AddOutputPort("True").Build();
        context.AddOutputPort("False").Build();

        context.AddInputPort<string>("Variable").Build();
    }
}
#endregion
[Serializable]
public class SwitchNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();

        context.AddOutputPort("True").Build();
        context.AddOutputPort("False").Build();

        context.AddInputPort<string>("Variable").Build();
        context.AddInputPort<int>("Value").Build();
    }
}

[Serializable]
public class FlagNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<string>("Variable").Build();
        context.AddInputPort<bool>("Value").Build();
    }
}

[Serializable]
public class IntNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<string>("Variable").Build();
        context.AddInputPort<int>("Value").Build();
    }
}


[Serializable]
public class SpawnNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<string>("Arena").Build(); 
        //context.AddInputPort<Transform>("Position").Build();
    }
}

#region Teleport
[Serializable]
public class TeleportNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        // Dialogue
        context.AddInputPort<string>("Scene").Build();
        context.AddInputPort<int>("WayPoint").Build();
    }
}
#endregion


[Serializable]
public class RemoveNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<string>("Character").Build();
    }
}

[Serializable]
public class ItemNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<int>("Count").Build();
        //context.AddInputPort<Item>("Item").Build();
    }
}

[Serializable]
public class CarochitoNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<CarochitoBase>("Carochito").Build();
        context.AddInputPort<int>("Level").Build();
    }
}

[Serializable]
public class CameraNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<string>("Camera").Build();
    }
}

[Serializable]
public class NormalCameraNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();
    }
}

[Serializable]
public class PositionNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("In").Build();
        context.AddOutputPort("Out").Build();

        context.AddInputPort<string>("Object").Build();
        context.AddInputPort<Vector3>("Position").Build();
        context.AddInputPort<Quaternion>("Rotation").Build();
    }
}

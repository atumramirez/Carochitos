using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public class BaseActionNode
{
    public string NodeID;
    public string NextNodeID;
    public virtual void Perform()
    {

    }
    public virtual void End()
    {

    }
}

[Serializable]
public class BaseDialogueAction : BaseActionNode
{
    public string DialogueText; // Texto
    public CharacterSprite CharacterSprite; // Sprites dos Persongens
    public Emotion Emotion;
    public AudioClip VoiceActing;
    public Side Side;
}

[Serializable]
public class CarochitoAction : BaseActionNode
{
    public CarochitoBase Carochito;
    public int Level;

    public override void Perform()
    {
        ActionManager.Instance.GiveCarochito();
    }
}

[Serializable]
public class SpeakAction : BaseDialogueAction
{
    public override void Perform()
    {
        ActionManager.Instance.dialogueManager.StartDialogue();
    }
}

[Serializable]
public class QuestionAction : BaseDialogueAction
{
    [Serializable]
    public class ChoiceData
    {
        public string ChoiceText;
        public string DestinationNodeID;
    }
    
    public List<ChoiceData> Choices = new();

    public override void Perform()
    {
        ActionManager.Instance.dialogueManager.StartDialogue();
    }
}

[Serializable]
public class RemoveSpriteAction: BaseActionNode
{
    public Side Side;

    public override void Perform()
    {
        ActionManager.Instance.dialogueManager.RemoveSprite(Side);
        ActionManager.Instance.EndAction();
    }
}

[Serializable]
public class GiveItemAction : BaseActionNode
{
    public int Count;
    //public Item Item;

    public override void Perform()
    {
        Debug.Log("" + Count);
        ActionManager.Instance.EndAction();
    }
}

[Serializable]
public class ChangeBackgroundAction : BaseActionNode 
{
    public Sprite Background;
    public int FadeIn;
    public int FadeOut;

    public override void Perform()
    {
        ActionManager.Instance.dialogueManager.ChangeBackground();

        ActionManager.Instance.EndAction();
    }
}

#region Fade
[Serializable]
public class FadeAction : BaseActionNode
{
    public Fade Fade;
    public int Duration;

    public override void Perform()
    {
        switch (Fade)
        {
            case Fade.FadeIn:
                ActionManager.Instance.dialogueManager.FadeIn();
                break;
            case Fade.FadeOut:
                ActionManager.Instance.dialogueManager.FadeOut();
                break;
        }
    }
}

public enum Fade
{
    FadeIn,
    FadeOut
}
#endregion

#region Wait
[Serializable]
public class WaitAction: BaseActionNode
{
    public int Duration;

    public override void Perform()
    {
        ActionManager.Instance.dialogueManager.Wait(Duration);
    }
}
#endregion

#region Condition
[Serializable]
public class ConditionAction : BaseActionNode
{
    public string Variable;

    public string nextTrueId;
    public string nextFalseId;

    public override void Perform()
    {
        ActionManager.Instance.EndAction();
    }
}

public enum Condition
{
    Equal,
    NotEqual
}


[Serializable]
public class FlagAction : BaseActionNode
{
    public string Variable;
    public bool Value;

    public override void Perform()
    {
        ActionManager.Instance.ChangeVariable();
    }
}

[Serializable]
public class SwitchAction : BaseActionNode
{
    public string Variable;
    public int Value;

    public string nextTrueId;
    public string nextFalseId;

    public override void Perform()
    {
        ActionManager.Instance.EndAction();
    }
}

[Serializable]
public class IntAction : BaseActionNode
{
    public string Variable;
    public int Value;

    public override void Perform()
    {
        ActionManager.Instance.ChangeVariable();
    }
}

[Serializable]
public class RemoveAction : BaseActionNode
{
    public string Character;
    public override void Perform()
    {
        ActionManager.Instance.RemoveCharacter();
    }
}

#endregion

[Serializable]
public class TeleportAction : BaseActionNode
{
    public string Scene;
    public int WayPoint;

    public override void Perform()
    {
        ActionManager.Instance.gameSceneManager.SwitchEnviromentScene(Scene, WayPoint);
        // ActionManager.Instance.EndAction();
    }
}

[Serializable]
public class SpawnAction : BaseActionNode
{
    public string Arena;
    //public Transform Position;
    public override void Perform()
    {
        ActionManager.Instance.StartBattle();
    }
}


[Serializable]
public class CameraAction : BaseActionNode
{
    public string Camera;
    public override void Perform()
    {
        ActionManager.Instance.FindCamera();
    }
}

[Serializable]
public class NormalCameraAction : BaseActionNode
{
    public override void Perform()
    {
        ActionManager.Instance.NormalCamera();

    }
}


[Serializable]
public class PositionAction : BaseActionNode
{
    public string Object;
    public Vector3 Position;
    public Quaternion Rotation;

    public override void Perform()
    {
        ActionManager.Instance.TelerportCharacter();

    }
}



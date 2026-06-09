using System;
using System.Collections.Generic;
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
    public CarochitoBase CarochitoBase;
    public int Level;
    public int MaxHealth;

    public override void Perform()
    {
        Carochito carochito = new(CarochitoBase, Level);
        Party.Instance.AddCarochito(carochito);
        ActionManager.Instance.EndAction();
    }
}

[Serializable]
public class SpeakAction : BaseDialogueAction
{
    public override void Perform()
    {
        DialogueManager.Instance.StartDialogue();
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
        DialogueManager.Instance.StartDialogue();
    }
}

[Serializable]
public class RemoveSpriteAction: BaseActionNode
{
    public Side Side;

    public override void Perform()
    {
        DialogueManager.Instance.RemoveSprite(Side);
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
        DialogueManager.Instance.ChangeBackground();

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
                DialogueManager.Instance.FadeIn();
                break;
            case Fade.FadeOut:
                DialogueManager.Instance.FadeOut();
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
        DialogueManager.Instance.Wait(Duration);
    }
}
#endregion

#region Condition
[Serializable]
public class ConditionAction : BaseActionNode
{
    public bool Variable;
    public Condition Condition;
    public bool Value;

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
#endregion

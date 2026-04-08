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
    public string SpeakerName;
    public string DialogueText;


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
public class GiveItemAction : BaseActionNode
{
    public int Count;
    public Item Item;

    public override void Perform()
    {
        Debug.Log("" + Count + Item);
        ActionManager.Instance.EndAction();
    }
}

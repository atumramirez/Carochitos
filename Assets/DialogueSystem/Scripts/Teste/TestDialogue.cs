using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public RuntimeDialogueGraph graph;
    public DialogueManager manager;

    public void Click()
    {
        manager.StartDialogue(graph);
    }
}

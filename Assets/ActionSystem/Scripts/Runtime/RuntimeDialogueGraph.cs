using UnityEngine;
using System.Collections.Generic;

public class RuntimeDialogueGraph : ScriptableObject
{
    public string EntryNodeID;

    [SerializeReference]
    public List<BaseActionNode> AllNodes = new();
}
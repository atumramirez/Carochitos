using UnityEngine;

[CreateAssetMenu()]
public class ItemBase : ScriptableObject
{
    public string _name;
    [TextArea]
    public string _description;
    public Pocket _pocket;
    public int cost;
}

public enum Pocket 
{
    Medicine,
    Capture,
    Key
}

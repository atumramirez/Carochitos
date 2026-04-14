using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    public string _name;

    [TextArea]
    public string _description;
}

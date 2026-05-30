using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Carochito/Criar novo Item")]
public class ItemBase : ScriptableObject
{
    [Header("Description")]
    public string _name;
    [TextArea]
    public string _description;

    [Header("Information")]
    public Sprite _sprite;
    public Pocket _pocket;
    public int _cost;
}

public enum Pocket 
{
    Medicine,
    Capture,
    Key
}

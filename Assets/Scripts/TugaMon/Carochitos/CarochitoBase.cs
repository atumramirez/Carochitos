using UnityEngine;

[CreateAssetMenu(fileName = "Carochito", menuName = "Carochitos/Criar novo Carochito")]
public class CarochitoBase : ScriptableObject
{
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [SerializeField] public Sprite _sprite;

    // Elemental Types 
    [SerializeField] ElementalTypes _elementalType1;
    [SerializeField] ElementalTypes _elementalType2;

    // Base Stats
    [SerializeField] int _maxHP;
    [SerializeField] int _attack;
    [SerializeField] int _defense;
    [SerializeField] int _speed;


    public int MaxHP { 
        get { return _maxHP; }
    }

    public string Name {
        get { return _name; }
    }

    public int Attack { 
        get { return _attack; }
    }

    public int Defense
    {
        get { return _defense; }
    }
}

public enum ElementalTypes
{
    None,
    Fire,
    Water,
    Wind
}

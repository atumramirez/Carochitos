using UnityEngine;

[CreateAssetMenu(fileName = "Carochito", menuName = "Carochitos/Criar novo Carochito")]
public class CarochitoBase : ScriptableObject
{
    [Header("Information")]
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [Header("Information")]
    [SerializeField] public Sprite _sprite;
    [SerializeField] GameObject _model;

    public GameObject Model { get { return _model; } }

    [Header("Elemental Types")]
    [SerializeField] ElementalTypes _elementalType1;
    [SerializeField] ElementalTypes _elementalType2;

    [Header("Stats")]
    [SerializeField] int _maxHealth;
    [Range(1, 400)]
    [SerializeField] int _attack;
    [Range(1, 400)]
    [SerializeField] int _defense;
    [Range(1, 400)]
    [SerializeField] int _speed;

    public int MaxHealth { get { return _maxHealth; } }
    public string Name { get { return _name; } }
    public int Attack { get { return _attack; } }
    public int Defense { get { return _defense; } }
    public int Speed { get { return _speed; } }
}

public enum ElementalTypes
{
    None,
    Fire,
    Water,
    Wind
}

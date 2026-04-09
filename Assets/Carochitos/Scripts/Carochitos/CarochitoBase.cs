using UnityEngine;

[CreateAssetMenu(fileName = "Carochito", menuName = "Carochitos/Criar novo Carochito")]
public class CarochitoBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] string _name;
    [SerializeField] int _number;

    [TextArea]
    [SerializeField] string _description;

    public string Name { get { return _name; }}
    public int Number { get { return _number; }}
    public string Description { get { return _description; }}

    [Header("Other Info")]
    [SerializeField] Sprite _sprite;
    [SerializeField] GameObject _model;

    public Sprite Sprite { get { return _sprite; }}
    public GameObject Model { get { return _model; }}


    [Header("Types")]
    [SerializeField] ElementalTypes _elementalType1;
    [SerializeField] ElementalTypes _elementalType2;

    public ElementalTypes ElementalType1 { get { return _elementalType1; }}
    public ElementalTypes ElementalType2 { get { return _elementalType2; }}

    [Header("Base Stats")]
    [SerializeField] int _maxHP;
    [SerializeField] int _attack;
    [SerializeField] int _defense;
    [SerializeField] int _speed;

    public int MaxHP { get { return _maxHP; }}
    public int Attack { get { return _attack; }}
    public int Defense { get { return _defense; }}
    public int Speed { get { return _speed; }}  
}

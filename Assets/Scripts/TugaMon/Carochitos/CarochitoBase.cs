using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carochito", menuName = "Carochito/Criar novo Carochito")]
public class CarochitoBase : ScriptableObject
{
    [Header("Information")]
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [Header("Database Settings")]
    [SerializeField] int _number;

    [SerializeField] bool _isCapured = false;
    public bool IsCaptured { get { return _isCapured; } set => _ = _isCapured; }

    [Header("Information")]
    [SerializeField] Sprite _sprite;
    
    [SerializeField] GameObject _model;

    [Header("Elemental Types")]
    [SerializeField] Elemental _elementalType1;
    [SerializeField] Elemental _elementalType2;

    [Header("Stats")]
    [SerializeField] int _maxHealth;
    [Range(1, 400)]
    [SerializeField] int _attack;
    [Range(1, 400)]
    [SerializeField] int _defense;
    [Range(1, 400)]
    [SerializeField] int _speed;

    [Header("Learnable Skills")]
    [SerializeField] List<LearnableSkills> learnableSkills;


    // Name and Description
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Number { get { return _number; } }


    // Info
    public Sprite Sprite { get { return _sprite; } }
    public GameObject Model { get { return _model; } }

    // Types
    public Elemental Type1 { get { return _elementalType1; } }
    public Elemental Type2 { get { return _elementalType2; } }


    // Stats
    public int MaxHealth { get { return _maxHealth; } }
    public int Attack { get { return _attack; } }
    public int Defense { get { return _defense; } }
    public int Speed { get { return _speed; } }

    // Skills
    public List<LearnableSkills> LearnableSkills { get { return learnableSkills; } }

}

[System.Serializable]
public class LearnableSkills
{
    [SerializeField] SkillBase skillBase;
    [SerializeField] int level;

    // Properties to expose the values
    public SkillBase SkillBase { get { return skillBase; } }
    public int Level { get { return level; } }
}

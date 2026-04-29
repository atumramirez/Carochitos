using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Carochito 
{
    [SerializeField] CarochitoBase _base;
    public CarochitoBase Base { get { return _base; } }

    [SerializeField] string _nickname;

    [Range(1, 100)]
    [SerializeField] int _level;
    public int Level { get { return _level; } }

    [Header("Health")]
    [SerializeField] int _currentHealth;
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    public Carochito(CarochitoBase chitoBase, int chitoLevel)
    {
        _base = chitoBase;
        _level = chitoLevel;
    }

    // Skills
    [SerializeField] List<Skill> _skills = new();
    public List<Skill> Skills { get { return _skills; } set { _skills = value; } }


    // Stats
    public string Name { get { if (_nickname != "") { return _nickname; } else { return _base.Name; } } }
    public int Attack { get { return (_base.Attack * _level / 100) + 5; } }
    public int Defense { get { return (_base.Defense * _level / 100) + 5; } }
    public int Speed { get { return (_base.Speed * _level / 100) + 5; } }
}


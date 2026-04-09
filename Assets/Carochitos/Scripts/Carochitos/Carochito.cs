using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Carochito 
{
    [Header("Basic Info")]
    [SerializeField] CarochitoBase _base;
    [SerializeField] string _nickname;
    [SerializeField] int _level;
   
    public CarochitoBase Base { get { return _base; } }
    public string Name { get { return _nickname; } }
    public int Level{ get { return _level; } }

    public Carochito(CarochitoBase chitoBase, int chitoLevel)
    {
        _base = chitoBase;
        _level = chitoLevel;
    }

    public List<Skill> Skills { get; set; }

    [Header("Stats")]
    [SerializeField] int _currentHP;
    public int CurrentHP { get { return _currentHP; }}
    public int MaxHP { get { return (_base.MaxHP * _level / 100) + 5; } }
    public int Attack { get { return (_base.Attack * _level / 100) + 5; }}
    public int Defense { get { return (_base.Defense * _level / 100) + 5; }}
    public int Speed { get { return (_base.Speed * _level / 100) + 5; } }
}

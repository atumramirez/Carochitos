using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Carochito 
{
    [SerializeField] CarochitoBase _base;
    [SerializeField] int _level;
    [SerializeField] int _currentHP;

    public CarochitoBase Base { 
        get { 
            return _base; } 
    }

    public int Level
    {
        get
        {
            return _level;
        }
    }

    public int CurrentHP
    {
        get
        {
            return _currentHP;
        }
    }

    public List<Skill> Skills { get; set; }
    public Carochito(CarochitoBase chitoBase, int chitoLevel)
    {
        _base = chitoBase;
        _level = chitoLevel;    
    }

    public int Attack {
        get { return (_base.Attack * _level / 100) + 5; }
    }

    public int Defense
    {
        get { return (_base.Defense * _level / 100) + 5; }
    }
}

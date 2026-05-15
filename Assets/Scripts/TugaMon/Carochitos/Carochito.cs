using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[System.Serializable]
public class Carochito 
{
    [SerializeField] CarochitoBase _base;
    [SerializeField] string _nickname;

    [Header("Health")]
    [SerializeField] int _currentHealth;
    [SerializeField] bool _isAlive;

    [Header("Level")]
    [Range(1, 100)]
    [SerializeField] int _level;
    [Range(1, 100)]
    [SerializeField] int _currentExp;

    [Header("Skills")]
    [SerializeField] List<Skill> _skill = new();

    public Carochito(CarochitoBase chitoBase, int chitoLevel)
    {
        _base = chitoBase;
        _level = chitoLevel;

        // Health
        _currentHealth = Base.MaxHealth;

        if (_currentHealth > 0)
        {
            _isAlive = true;
        }
        else
        {
            _isAlive = false;
        }

        // Make sure the list is empty when it's created
        _skill = new();

        // Spawn with Skills based on the Level
        foreach (var skill in _base.LearnableSkills)
        {
            if (skill.Level <= Level)
            {
                _skill.Add(new Skill(skill.SkillBase));
            }

            if (_skill.Count >= 4)
            {
                break;
            }
        }
    }

    public void GetExp(int exp)
    {
        _currentExp += exp;

        Debug.Log("You got " + exp + " points fo experience!");

        if (_currentExp >= 100)
        {
            LevelUp();
        }   
    }

    public void LevelUp()
    {
        // Leveling Up

        int escessExp = _currentExp - 100;
        _level += 1;
        _currentExp = escessExp;

        Debug.Log("You Leveled Up! Now you are at Level: " + _level + "!");

        // Add New Skills when getting new Level

        int _currentLevel = _level;

        foreach (var skill in _base.LearnableSkills)
        {
            if (skill.Level == _currentLevel)
            {
                _skill.Add(new Skill(skill.SkillBase));

                Debug.Log("You learned a new Move!");
            }

            if (_skill.Count >= 4)
            {
                break;
            }
        }
    }

    #region Properties
    public CarochitoBase Base { get { return _base; } }
    public int Level { get { return _level; } }
    public List<Skill> Skill { get { return _skill; } set { _skill = value; } }
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }
    public int CurrentExp { get { return _currentExp; } set { _currentExp = value; } }
    public string Name { get { if (_nickname != "") { return _nickname; } else { return _base.Name; } } }
    public int Attack { get { return (_base.Attack * _level / 100) + 5; } }
    public int Defense { get { return (_base.Defense * _level / 100) + 5; } }
    public int Speed { get { return (_base.Speed * _level / 100) + 5; } }
    #endregion
}


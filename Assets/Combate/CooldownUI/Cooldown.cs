using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [Serializable]
    public class SkillCooldown
    {
        public Skill skill;
        public Image image;
    }

    public List<SkillCooldown> _cooldownIcons = new();

    public void StartCooldown()
    {
        _cooldownIcons[0].image.fillAmount = 1;
    }

    public void Update()
    {
        if (_cooldownIcons[0].image.fillAmount > 0)
        {
            _cooldownIcons[0].image.fillAmount -= (1f / _cooldownIcons[0].skill.Cooldown) * Time.deltaTime;
        }
    }
}

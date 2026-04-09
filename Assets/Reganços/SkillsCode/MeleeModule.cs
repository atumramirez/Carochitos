using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillModule", menuName = "Skill/SkillModule")]
public class MeleeModule : ScriptableObject
{
    public Vector3 hitboxSize = new(1, 1, 1);
    public float duration = 0.2f;
    public int damage = 10;
    
}

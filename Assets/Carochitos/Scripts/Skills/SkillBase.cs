using UnityEngine;

public class SkillBase : ScriptableObject
{
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [SerializeField] ElementalTypes _elementalTypes;

    [SerializeField] int power;
    [SerializeField] int coldown;

    // Categoria de Ataques
    // Status ou Buffs
}

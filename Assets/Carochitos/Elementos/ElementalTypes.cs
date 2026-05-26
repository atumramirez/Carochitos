using UnityEngine;

[CreateAssetMenu(fileName = "Carochito", menuName = "Carochito/Criar novo Elemento")]
public class Elemental : ScriptableObject
{
    [Header("Sprite")]
    [SerializeField] Sprite _icon;

    [Header("Element")]
    [SerializeField] ElementalTypes _type;

    [Header("Reactions")]
    [SerializeField] ElementalTypes[] weaknesses;
    [SerializeField] ElementalTypes[] resistances;
}

public enum ElementalTypes
{
    None,
    Fire,
    Water,
    Wind,
    Beast,
    Spooky,
    Gold,
    Energy,
    Nature
}

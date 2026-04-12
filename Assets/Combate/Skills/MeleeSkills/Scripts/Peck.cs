using UnityEngine;

[CreateAssetMenu(fileName = "Criar nova Abilidade", menuName = "Skill/Abilidade")]
public class Peck : MeleeSkill
{
    public override void Activate(GameObject parent)
    {
        Debug.Log("Olá");
    }
}

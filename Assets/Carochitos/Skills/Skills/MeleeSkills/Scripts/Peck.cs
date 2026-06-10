using UnityEngine;

[CreateAssetMenu(fileName = "Criar nova Abilidade", menuName = "Skill/Abilidade")]
public class Peck: MeleeSkill
{

    public override void Activate(CarochitoBattler carochitoBattler)
    {
        /*
        GameObject hitboxF = Instantiate(hurtBox.gameObject, parent.transform.TransformPoint(new Vector3(0, 0, 1)), parent.transform.rotation, parent.transform);
        hitboxF.transform.localScale = modules[0].hitboxSize;

        HurtBox hitboxDamage = hitboxF.GetComponent<HurtBox>();

        /*
        hitboxDamage.owner = parent.GetComponent<CarochitoHandler>();
        hitboxDamage.skill = this;
        

        Destroy(hitboxF, ActiveTime);
        */
    }
}

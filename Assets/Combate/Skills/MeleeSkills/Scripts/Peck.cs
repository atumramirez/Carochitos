using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Criar nova Abilidade", menuName = "Skill/Abilidade")]
public class Peck : MeleeSkill
{
    public override void Activate(GameObject parent)
    {

    }

    IEnumerator AttackWithSkill(GameObject parent)
    {
        foreach(MeleeModule mod in modules)
        {
            InstantiateAttack(mod, parent);
            yield return new WaitForSeconds(mod.duration);
        }   
    }

    void InstantiateAttack(MeleeModule mod, GameObject parent)
    {
        GameObject hitboxF = Instantiate(hurtBox.gameObject, parent.transform.TransformPoint(new Vector3(0, 0, 1)), parent.transform.rotation, parent.transform);
        hitboxF.transform.localScale = mod.hitboxSize;

        HurtBox hitboxDamage = hitboxF.GetComponent<HurtBox>();
        hitboxDamage.owner = parent.GetComponent<CarochitoHandler>();
        
        Destroy(hitboxF, mod.duration);
    } 
    
}

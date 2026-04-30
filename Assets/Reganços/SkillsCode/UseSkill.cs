using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill: MonoBehaviour
{
    [Header("Skills")]
    public List<MeleeSkill> skills;
    public MeleeSkill currentSkill;

    [Header("Modules")]
    public List<MeleeModule> meleeModules;
    public MeleeModule currentModule;

    [Header("Cooldown")]
    public bool onColdown;
    public GameObject hitbox;

    [Header("Loucuras")]
    public CarochitoHandler owner;
    public SkillBase skill;

    void Start()
    {
        onColdown = false;
        currentSkill = skills[0];
        currentModule = currentSkill.modules[0];
    }

    public void UseCurrentSkill()
    {
        if (onColdown == false)
        {
            StartCoroutine(AttackWithSkill());
            onColdown = true;
        }
    }

    IEnumerator AttackWithSkill()
    {
        foreach(MeleeModule mod in currentSkill.modules)
        {
            // Debug.Log("Current Skill" + mod.name);
                
            InstantiateAttack(mod);

            yield return new WaitForSeconds(mod.duration);
        }
        yield return new WaitForSeconds(currentSkill.Cooldown);
        onColdown = false;
        
    }

    void InstantiateAttack(MeleeModule mod)
    {
        GameObject hitboxF = Instantiate(hitbox, transform.TransformPoint(new Vector3(0, 0, 1)), transform.rotation, transform);
        hitboxF.transform.localScale = mod.hitboxSize;

        HurtBox hitboxDamage = hitboxF.GetComponent<HurtBox>();
        hitboxDamage.owner = owner;
        
        Destroy(hitboxF, mod.duration);
    }
}

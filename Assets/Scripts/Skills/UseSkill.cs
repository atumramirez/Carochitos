using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UseSkill : MonoBehaviour
{
    public List<SkillBases> skills;
    public SkillBases currentSkill;
    public List<MeleeModule> meleeModules;
    public MeleeModule currentModule;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSkill = skills[0];
        currentModule = currentSkill.modules[0];
    }

    // Update is called once per frame
    void Update()
    {
        AttackWithSkill();
    }

    void AttackWithSkill()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tentou atavar");
            GameObject hitbox = new GameObject("Hitbox");
            hitbox.transform.position = transform.position + transform.forward * 1f;

            BoxCollider col = hitbox.AddComponent<BoxCollider>();
            col.isTrigger = true;
            col.size = currentModule.hitboxSize;
            GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            visual.transform.SetParent(hitbox.transform);
            visual.transform.localPosition = Vector3.zero;
            visual.transform.localRotation = Quaternion.identity;
            visual.transform.localScale = currentModule.hitboxSize;

            Destroy(visual.GetComponent<Collider>()); // remove extra collider

            

            Vector3 center = transform.position + transform.forward * 1f;

           
            Destroy(hitbox, currentModule.duration);
        }
    }

    


}

using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public List<SkillBase> _skills = new();

    float _cooldownTime;
    float _activeTime;

    public Cooldown cooldown;

    enum SkillState 
    {
        Ready,
        Active,
        Cooldown,
    }

    SkillState _state = SkillState.Ready;

    private void Update()
    {
        switch (_state)
        {
            case SkillState.Ready:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _skills[0].Activate(gameObject);
                    _state = SkillState.Active;
                    _activeTime = _skills[0].ActiveTime;

                }
                break;

            case SkillState.Active:

                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else 
                {
                    _state = SkillState.Cooldown;
                    cooldown.StartCooldown();
                    _cooldownTime = _skills[0].Cooldown;
                }
                break;

            case SkillState.Cooldown:
                if (_cooldownTime > 0)
                {
                    _cooldownTime -= Time.deltaTime;
                }
                else
                {
                    _state = SkillState.Ready;
                }
                break;
        }
    }
}

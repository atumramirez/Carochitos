using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IconAbility : MonoBehaviour
{
    public Image icon;
    public Image cooldownIcon;

    public void SetUp(Skill skill)
    {
        icon.sprite = skill.Base.SkillIcon;
        cooldownIcon.sprite = skill.Base.SkillIcon;
        cooldownIcon.fillAmount = 0;
    }

    public void StartCooldown(Skill skill)
    {
        StartCoroutine(DecreaseFill(skill));
    }

    private IEnumerator DecreaseFill(Skill skill)
    {
        float startFill = 1;
        float duration = skill.Base.FullCooldown;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cooldownIcon.fillAmount = Mathf.Lerp(startFill, 0f, elapsed / duration);
            yield return null;
        }

        cooldownIcon.fillAmount = 0f;
    }
}

using UnityEngine;

public class HurtBox : MonoBehaviour
{
    private CarochitoBattler _carochitoBattler;
    public CarochitoBattler CarochitoBattler { get { return _carochitoBattler; } }

    public void SetUp(CarochitoBattler carochitoBattler)
    {
        _carochitoBattler = carochitoBattler;
    }

    public void ReceiveHit(CarochitoBattler attacker, SkillBase skill)
    {
        if (_carochitoBattler != null)
        {
            _carochitoBattler.TakeDamage(attacker, skill);
        }
    }
}

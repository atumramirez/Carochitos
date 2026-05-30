using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public GameObject boxPrefab;
    public CarochitoBattler BattlerOwner;
    public SkillBase SkillUsed;

    public Vector3 size;
    public float duration;

    public void SetUp(CarochitoBattler carochitoBattler, SkillBase skill)
    {
        BattlerOwner = carochitoBattler;
        SkillUsed = skill;
    }
    public void Parameters(Vector3 fSize, float fDuration)
    {
        size = fSize;
        duration = fDuration;
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject boxCravo = Instantiate(boxPrefab, transform.position, Quaternion.identity);
        boxCravo.transform.localScale = size;
        if (boxCravo.TryGetComponent<HitBoxArea>(out var hitBox))
        {
            hitBox.SetUp(BattlerOwner, SkillUsed);
            Destroy(boxCravo,duration);
            Destroy(gameObject);
        }
    }
}

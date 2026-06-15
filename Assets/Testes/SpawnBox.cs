using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public GameObject boxPrefab;
    public CarochitoBattler BattlerOwner;
    public SkillBase SkillUsed;
    public GameObject particles;

    public Vector3 size;
    public float duration;

    public void SetUp(CarochitoBattler carochitoBattler, SkillBase skill)
    {
        BattlerOwner = carochitoBattler;
        SkillUsed = skill;
    }
    public void Parameters(Vector3 fSize, float fDuration, GameObject fparticles)
    {
        size = fSize;
        duration = fDuration;
        particles = fparticles;
    }
    public void GetEffect(GameObject fEffect)
    {
        particles = fEffect;
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject projectile = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(projectile, 5f);
        GameObject boxCravo = Instantiate(boxPrefab, transform.position, Quaternion.identity);
        //particles.SetActive(true);
        if (boxCravo.TryGetComponent<HitBoxArea>(out var hitBox))
        {
            hitBox.SetUp(BattlerOwner, SkillUsed);
            Destroy(boxCravo,duration);
            Destroy(gameObject);
        }
    }
}

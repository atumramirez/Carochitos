using UnityEngine;

public class SpawnerProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject fParticle;

    public float fireRate = 0.2f;
    private float nextFireTime;
    public Vector3 finalSize;
    public float finalDuration;

    public CarochitoBattler a;
    public SkillBase b;
    public AudioClip soundE;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        SoundManager.instance.PlayClip(soundE, transform, 0.75f);

        if (projectile.TryGetComponent<SpawnBox>(out var spawnBox))
        {
            spawnBox.SetUp(a, b);
            spawnBox.Parameters(finalSize,finalDuration,fParticle);
            spawnBox.GetEffect(fParticle);
        }

    }
}

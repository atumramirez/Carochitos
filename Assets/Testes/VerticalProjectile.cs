using UnityEngine;

public class VerticalProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float nextFireTime;
    public Vector3 finalSize;
    public float finalDuration;

    public CarochitoBattler a;
    public SkillBase b;

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


        if (projectile.TryGetComponent<SpawnBox>(out var spawnBox))
        {
            spawnBox.SetUp(a, b);
            spawnBox.Parameters(finalSize, finalDuration);
        }

    }
}

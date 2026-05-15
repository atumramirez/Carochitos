using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float nextFireTime;

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

        // Set Up

        if (projectile.TryGetComponent<HitBox>(out var hitBox))
        {
            hitBox.SetUp(a, b);
        }
        
    }
}

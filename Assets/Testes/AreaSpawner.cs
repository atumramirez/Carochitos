using System.Drawing;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public GameObject areaPrefab;
    public GameObject particles;
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
        GameObject projectile = Instantiate(areaPrefab, firePoint.position, Quaternion.identity);
        projectile.transform.localScale = finalSize;
        //GameObject particle = Instantiate(areaPrefab, firePoint.position, Quaternion.identity);

        if (projectile.TryGetComponent<HitBoxArea>(out var spawnBox))
        {
            spawnBox.SetUp(a, b);
            spawnBox.GetEffect(particles);
            
            
        }

    }
}

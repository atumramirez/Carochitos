using UnityEngine;
using System.Collections;
public class FlameSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public ParticleSystem particles;
    public CarochitoBattler a;
    public SkillBase b;
    public float duration = 5f;
    public float interval = 0.5f;
    public AudioClip soundE;

    private bool isFiring;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFiring)
        {
            StartCoroutine(FireFlamethrower());
            particles.Play();
        }
    }

    IEnumerator FireFlamethrower()
    {
        SoundManager.instance.PlayClip(soundE, transform, 0.75f);
        isFiring = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            SpawnProjectile();

            yield return new WaitForSeconds(interval);

            elapsed += interval;
        }

        isFiring = false;
        particles.Stop();
    }

    void SpawnProjectile()
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Set Up
        if (projectile.TryGetComponent<HitBox>(out var hitBox))
        {
            hitBox.SetUp(a, b);
        }
    }
}

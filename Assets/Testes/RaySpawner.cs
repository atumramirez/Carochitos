using UnityEngine;

public class RaySpawner : MonoBehaviour
{
    
    public CarochitoBattler a;
    public SkillBase b;
    public Transform firePoint;
    public bool hit;
    public float size;
    public LayerMask layer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Debug.Log("Try Shoot");
            ShootRay();
        }
    }

    void ShootRay()
    {
        RaycastHit hit;
        if (Physics.SphereCast(firePoint.position, size, transform.forward, out hit, 100, layer))
        {
            Debug.Log("Raio Acertou Inimigo");
            GameObject enemy = hit.transform.gameObject;
            if (enemy.TryGetComponent<HurtBox>(out var hurtbox))
            {
                CarochitoBattler victim = hurtbox.CarochitoBattler;

                if (victim.Carochito.IsAlive == true)
                {
                    hurtbox.ReceiveHit(a, b);
                }
            }
        }
    }

    
}

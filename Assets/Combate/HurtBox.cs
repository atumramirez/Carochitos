using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public int damage = 10;

    public CarochitoHandler owner;
    public Skill skill;

    private void OnTriggerEnter(Collider other)
    {
        CarochitoHandler enemyCarochito = other.GetComponentInParent<CarochitoHandler>();

        if (enemyCarochito != null && enemyCarochito != owner)
        {
            enemyCarochito.TakeDamage(owner, skill);
            Debug.Log("Acertou Playboy");
        }
    }
}

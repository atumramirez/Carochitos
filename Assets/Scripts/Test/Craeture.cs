using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName;

    public Carochito carochito;
    public int maxHp;
    public int currentHp;
    public int damage;

    public void Capture()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        maxHp = carochito.CurrentHP;
        currentHp = maxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attack")
        {
            currentHp = currentHp - damage;
        }
    }
}
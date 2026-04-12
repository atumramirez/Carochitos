using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName;

    public Carochito carochito;
    public int maxHp = 30;
    public int currentHp;
    public int damage = 10;

    public void Capture()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        maxHp = carochito.CurrentHealth;
        currentHp = maxHp;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentHp = currentHp - 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attack")
        {
            currentHp = currentHp - damage;
        }
    }
}
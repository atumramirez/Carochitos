using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName;

    public Carochito carochito;

    public void Capture()
    {
        Destroy(gameObject);
    }
}
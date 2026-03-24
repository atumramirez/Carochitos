using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName;

    public void Capture()
    {
        Destroy(gameObject);
    }
}
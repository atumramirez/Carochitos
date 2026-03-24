using UnityEngine;

public class CustomizationSettings : MonoBehaviour
{
    public GameObject Object;

    public GameObject Object2;

    public static CustomizationSettings instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

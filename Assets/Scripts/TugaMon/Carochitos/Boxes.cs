using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public static Boxes Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<Carochito> Box1;
    public void AddCarochito(Carochito carochito)
    {
        Box1.Add(carochito);
    }
}

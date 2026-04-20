using JetBrains.Annotations;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject[] carochitos;
    public float radius;
    public float maxCar;
    public GameObject[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            SpawnCarochito();
        }
    }

    void SpawnCarochito()
    {
        GameObject currentChito = carochitos[Random.Range(0, carochitos.Length)];
        GameObject currentPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawn = new Vector3(currentPoint.transform.position.x + Random.Range(-radius, radius), currentPoint.transform.position.y, currentPoint.transform.position.z + Random.Range(-radius, radius));
        if (currentPoint.transform.childCount < maxCar)
        {
            GameObject newChiton = Instantiate(currentChito, spawn, Quaternion.identity, currentPoint.transform);
        }
        
    }

    void FillPoints()
    {
        
        float maxiCaros = maxCar * spawnPoints.Length;
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject currentChito = carochitos[Random.Range(0, carochitos.Length)];
            GameObject currentPoint = spawnPoints[i];
            
            for (int j = 0; j < maxCar; j++)
            {
                Vector3 spawn = new Vector3(currentPoint.transform.position.x + Random.Range(-radius, radius), currentPoint.transform.position.y, currentPoint.transform.position.z + Random.Range(-radius, radius));
                Instantiate(currentChito, spawn, Quaternion.identity, currentPoint.transform);
            }
        }
    }
}

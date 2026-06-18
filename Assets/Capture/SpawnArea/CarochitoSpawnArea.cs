using UnityEngine;

public class CarochitoSpawnArea : MonoBehaviour
{
    [Header("Carochito")]
    public CarochitoBase _base;
    private int _level;

    [Header("Level")]
    public int maxLevel;
    public int minLevel;

    [Header("Intervals")]
    public int maxSpawned = 3;

    [Header("Spawn Area")]
    public Vector3 areaSize = new(10f, 0f, 10f);

    private void Start()
    {
        TrySpawn();
    }

    public void TrySpawn()
    {
        for (var i = 0; i < maxSpawned; i++)
        {
            int _level = Random.Range(minLevel, maxLevel + 1);
            Carochito carochito = new(_base, _level);

            Vector3 randomPosition = transform.position + new Vector3
            (
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
            );

            GameObject carochitoModel = Instantiate(carochito.Base.Model, randomPosition, Quaternion.identity);
            carochitoModel.GetComponent<CarochitoBattler>().SetUp(carochito);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}

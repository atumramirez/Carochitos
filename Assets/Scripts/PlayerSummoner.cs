using UnityEngine;

public class PlayerSummoner : MonoBehaviour
{
    public CarochitoParty _carochitoParty;
    private GameObject _currentInstance;

    public void ActivateSummon()
    {
        if (_currentInstance != null)
        {
            TakeBack();
        }
        else
        {
            SpawnPrefab();
        }
    }

    public void SpawnPrefab()
    {
        _currentInstance = Instantiate(_carochitoParty.carochitos[0].Base.Model, Vector3.zero, Quaternion.identity);

        // Spawna o modelo do personagem
        // Spawna na posição desejada
        
        if (_currentInstance.TryGetComponent<CarochitoHandle>(out var data))
        {
            data.carochito = _carochitoParty.carochitos[0];
        }
        else
        {
            Debug.LogWarning("MyPrefabData script not found on prefab!");
        }
    }

    public void TakeBack()
    {
        if (_currentInstance != null)
        {
            Destroy(_currentInstance);
            _currentInstance = null;
        }
        else
        {
            Debug.LogWarning("No instance to remove!");
        }
    }
}

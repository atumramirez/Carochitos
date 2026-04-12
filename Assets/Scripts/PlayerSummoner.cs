using UnityEngine;

public class PlayerSummoner : MonoBehaviour
{
    private GameObject _currentInstance;
    public CharacterSwap _characterSwap;

    public Transform spawnPoint;

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
        if (CarochitoParty.Instance.carochitos.Count > 0)
        {
            CarochitoParty.Instance.currentCarochito ??= CarochitoParty.Instance.carochitos[0];

            _currentInstance = Instantiate(
            CarochitoParty.Instance.currentCarochito.Base.Model,
            spawnPoint.position,
            spawnPoint.rotation
             );

            if (_currentInstance.TryGetComponent<CarochitoHandler>(out var data))
            {
                data.carochito = CarochitoParty.Instance.currentCarochito;
                _characterSwap.AddCharacter(data.transform);
            }
            else
            {
                Debug.LogWarning("MyPrefabData script not found on prefab!");
            }
        }
    }

    public void TakeBack()
    {
        if (_currentInstance != null)
        {
            _characterSwap.RemoveCharacter(_currentInstance.transform);
            Destroy(_currentInstance);
            _currentInstance = null;
        }
        else
        {
            Debug.LogWarning("No instance to remove!");
        }
    }
}

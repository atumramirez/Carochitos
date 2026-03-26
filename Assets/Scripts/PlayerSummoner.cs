using UnityEngine;

public class PlayerSummoner : MonoBehaviour
{
    private GameObject _currentInstance;
    public CharacterSwap _characterSwap;

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
        _currentInstance = Instantiate(CarochitoParty.Instance.currentCarochito.Base.Model, Vector3.zero, Quaternion.identity);

        
        if (_currentInstance.TryGetComponent<CarochitoHandle>(out var data))
        {
            data.carochito = CarochitoParty.Instance.currentCarochito;
            _characterSwap.AddCharacter(data.transform);
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

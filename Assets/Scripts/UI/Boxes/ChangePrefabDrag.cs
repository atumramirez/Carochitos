using UnityEngine;

public class ChangePrefabDrag : MonoBehaviour
{
    public GameObject _partyPrefab;
    public GameObject _otherPrefab;

    public void SwapToOtherPrefab(Transform parent)
    {
        if (_otherPrefab == null)
        {
            Debug.LogWarning("No prefab assigned to swap!");
            return;
        }

        GameObject newItem = Instantiate(_otherPrefab, parent);
        newItem.transform.SetAsLastSibling();

        newItem.transform.position = transform.position;

        Destroy(parent.GetChild(0));
    }
}

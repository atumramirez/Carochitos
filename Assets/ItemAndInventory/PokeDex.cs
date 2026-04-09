using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PokeDex : MonoBehaviour
{
    [Header("Carochitos Captured")]
    public int _amountCapured;
    public int _total;

    [Header("List")]
    public List<PokeDexEntry> collectedItems = new();

    [ContextMenu("Collect ScriptableObjects")]
    public void CollectScriptableObjects()
    {
        collectedItems.Clear();

#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:CarochitoBase");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CarochitoBase item = AssetDatabase.LoadAssetAtPath<CarochitoBase>(path);

            PokeDexEntry slot = new(item, 0);

            if (item != null && slot != null)
            {
                collectedItems.Add(slot);
                _total += 1;
            }
        }

        collectedItems = collectedItems.OrderBy(slot => slot.CarochitoBase.Number).ToList();
        Debug.Log($"Collected {collectedItems.Count} ItemData ScriptableObjects.");
#else
        Debug.LogWarning("This only works in the Unity Editor.");
#endif
    }
}

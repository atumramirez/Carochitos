#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CarochitoDatabaseBuilder
{
    [MenuItem("Carochitos/Build Carochito Database")]
    public static void BuildDatabase()
    {
        // Encontra todos os Carochitos dentro do projeto, depois mudar para so encontrar Carochitos que estejam numa pasta especifica
        string[] guids = AssetDatabase.FindAssets("t:CarochitoBase");

        var carochitos = guids
            .Select(g => AssetDatabase.LoadAssetAtPath<CarochitoBase>(AssetDatabase.GUIDToAssetPath(g)))
            .OrderBy(c => c.Number)
            .ToList();

        // Criar ou mudar uma Database
        string path = "Assets/CarochitoDatabase.asset";
        var db = AssetDatabase.LoadAssetAtPath<CarochitoDatabaseAsset>(path);

        if (db == null)
        {
            db = ScriptableObject.CreateInstance<CarochitoDatabaseAsset>();
            AssetDatabase.CreateAsset(db, path);
        }

        db.carochitos = carochitos;

        EditorUtility.SetDirty(db);
        AssetDatabase.SaveAssets();

        Debug.Log("Carochito Database built!");
    }
}
#endif
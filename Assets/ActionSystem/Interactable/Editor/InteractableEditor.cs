using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Interactable interactable = (Interactable)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Create New Dialogue Graph"))
        {
            CreateDialogueGraph(interactable);
        }
    }

    private void CreateDialogueGraph(Interactable interactable)
    {
        // Create new ScriptableObject
        RuntimeDialogueGraph newGraph = ScriptableObject.CreateInstance<RuntimeDialogueGraph>();

        // Ask where to save it
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Dialogue Graph",
            "NewDialogueGraph",
            "asset",
            "Choose location for the dialogue graph"
        );

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newGraph, path);
            AssetDatabase.SaveAssets();

            // Assign it automatically
            interactable.dialogueGraph = newGraph;

            EditorUtility.SetDirty(interactable);

            Debug.Log("New Dialogue Graph created and assigned!");
        }
    }
}
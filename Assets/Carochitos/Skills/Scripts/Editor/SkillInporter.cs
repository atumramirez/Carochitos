using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class CreateSkillScript
{
    [MenuItem("Assets/Create/Carochito/Criar nova Skill", false, 81)]
    public static void CreateScript()
    {
        string path = GetSelectedPathOrFallback();

        string template =

@"using UnityEngine;

[CreateAssetMenu(fileName = ""Skill"", menuName = ""Carochito/Skill/Criar nova Skill"")]

public class #SCRIPTNAME# : SkillBase
{
    public override void Activate(GameObject parent)
    {
        base.Activate(parent);
    }
}";

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<CreateSkillScriptAction>(),
            Path.Combine(path, "NewSkill.cs"),
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
            template
        );
    }

    static string GetSelectedPathOrFallback()
    {
        string path = "Assets";

        foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);

            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }

            break;
        }

        return path;
    }
}

public class CreateSkillScriptAction : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        string fileName = Path.GetFileNameWithoutExtension(pathName);

        string scriptText = resourceFile.Replace("#SCRIPTNAME#", fileName);

        File.WriteAllText(pathName, scriptText);

        AssetDatabase.Refresh();

        Object asset = AssetDatabase.LoadAssetAtPath<Object>(pathName);
        ProjectWindowUtil.ShowCreatedAsset(asset);
    }
}
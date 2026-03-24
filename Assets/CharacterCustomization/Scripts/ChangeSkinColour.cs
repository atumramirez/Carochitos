using UnityEngine;

public class ChangeSkinColour : MonoBehaviour
{
    public Material Material1;

    public void ChangeColour()
    {
        CustomizationSettings.instance.Object.GetComponent<SkinnedMeshRenderer>().material = Material1;
    }

    public void ChangeColour2()
    {
        CustomizationSettings.instance.Object2.GetComponent<SkinnedMeshRenderer>().material = Material1;
    }
}

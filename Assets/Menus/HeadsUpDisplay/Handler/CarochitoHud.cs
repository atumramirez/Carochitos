using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarochitoHud : MonoBehaviour
{
    public List<SkillCooldown> icons;
    public List<Image> images;

    public void SetUp()
    {
        Debug.Log("Set Up");
    }
}

using System.Collections.Generic;
using UnityEngine;

public class CustomizationSettings : MonoBehaviour
{
    public void Start()
    {
        Basic();
    }

    [Header("Skin Tone")]
    // Skin Tones
    public List<Material> skinTones;

    // Body Parts
    public GameObject body;

    public void ChangeSkinTone(int skinTone)
    {
        if (skinTone < 0 || skinTone >= skinTones.Count)
        {
            return;
        }

        body.GetComponent<SkinnedMeshRenderer>().material = skinTones[skinTone];
    }

    [Header("Hair")]
    // Hairs
    public List<GameObject> hairs;

    // Hair Colour
    public List<Material> hairColours;

    public void ChangeHair(int hair)
    {
        if (hair < 0 || hair >= hairs.Count)
        {
            return;
        }

        for (int i = 0; i < hairs.Count; i++)
        {
            if (i == hair)
            {
                hairs[i].SetActive(true);
            }
            else
            {
                hairs[i].SetActive(false);
            }
        }
    }

    public void ChangeHairColour(int colour)
    {
        if (colour < 0 || colour >= hairColours.Count)
        {
            return;
        }

        foreach (GameObject hair in hairs)
        {
            hair.GetComponent<SkinnedMeshRenderer>().material = hairColours[colour];
        }
    }

    [Header("Eyes")]

    // Eyes
    public List<Material> eyeColours;

    public GameObject eye;

    public void ChangeEyeColour(int colour)
    {
        if (colour < 0 || colour >= eyeColours.Count)
        {
            return;
        }

        eye.GetComponent<SkinnedMeshRenderer>().material = eyeColours[colour];
    }

    [Header("Mouths")]

    // Mouths
    public List<Material> mouthShapes;
    public GameObject mouth;

    public void ChangeMouthShape(int mouthShape)
    {
        if (mouthShape < 0 || mouthShape >= mouthShapes.Count)
        {
            return;
        }

        mouth.GetComponent<SkinnedMeshRenderer>().material = mouthShapes[mouthShape];
    }


    [Header("Clothing")]
    public List<Material> clothings;
    public GameObject cloth;

    public void ChangeClothing(int cloths)
    {
        if (cloths < 0 || cloths >= clothings.Count)
        {
            return;
        }

        cloth.GetComponent<SkinnedMeshRenderer>().material = clothings[cloths];
    }


    // Basic Fit
    public void Basic()
    {
        ChangeSkinTone(0);
        ChangeHair(0);
        ChangeHairColour(0);
        ChangeEyeColour(0);
        ChangeMouthShape(0);
        ChangeClothing(0);
    }
}

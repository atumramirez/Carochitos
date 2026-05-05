using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHair : MonoBehaviour
{
    [Header("Skin")]
    public GameObject skin;

    
    [Header("Hair")]
    public List<GameObject> hair;

    [Header("UI Setup")]
    public Transform buttonParent;   
    public Button buttonPrefab;

    private void Start()
    {
        ActivateHair(0);

        // Generate Hair Buttons
        GenerateHairButtons();
    }

    public void ActivateHair(int index)
    {
        for (int i = 0; i < hair.Count; i++)
        {
            if (hair[i] != null)
            {
                hair[i].SetActive(i == index);
            }
        }
    }

    // ---- GENERATE BUTTONS ----
    public void GenerateHairButtons()
    {
        // Destruir os botões anteriores 
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // 
        for (int i = 0; i < hair.Count; i++)
        {
            int index = i; 

            Button newButton = Instantiate(buttonPrefab, buttonParent);

            Text text = newButton.GetComponentInChildren<Text>();

            if (text != null && hair[i] != null)
            {
                text.text = hair[i].name;
            }

            newButton.onClick.AddListener(() => ActivateHair(index));
        }
    }

    public void GenerateSkinButton()
    {
        
    }
}

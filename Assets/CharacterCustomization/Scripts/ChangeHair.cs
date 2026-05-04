using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHair : MonoBehaviour
{
    public List<GameObject> hair;

    [Header("UI Setup")]
    public Transform buttonParent;   // UI container (e.g. a Vertical Layout Group)
    public Button buttonPrefab;

    private void Start()
    {
        ActivateOnly(0);
        GenerateButtons();
    }

    public void ActivateOnly(int index)
    {
        for (int i = 0; i < hair.Count; i++)
        {
            // 
            if (hair[i] != null)
            {
                hair[i].SetActive(i == index);
            }
        }
    }

    // Create buttons based on the number of objects
    public void GenerateButtons()
    {
        // Optional: clear existing buttons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hair.Count; i++)
        {
            int index = i; // Important: capture index for the listener

            Button newButton = Instantiate(buttonPrefab, buttonParent);

            // Set button text (if it has a Text component)
            Text text = newButton.GetComponentInChildren<Text>();
            if (text != null && hair[i] != null)
            {
                text.text = hair[i].name;
            }

            // Add click event
            newButton.onClick.AddListener(() => ActivateOnly(index));
        }
    }
}

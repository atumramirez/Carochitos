using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameValidator : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button confirmButton;

    void Start()
    {
        confirmButton.interactable = false;
        inputField.onValueChanged.AddListener(CheckInput);
    }

    void CheckInput(string text)
    {
        bool isValid = !string.IsNullOrWhiteSpace(text);

        confirmButton.interactable = isValid;
    }
}

using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 23; i++)
        {
            Debug.LogError("Error alert #" + (i + 1));
        }

        for (int i = 0; i < 4; i++)
        {
            Debug.LogWarning("Warning alert #" + (i + 1));
        }

        for (int i = 0; i < 13; i++)
        {
            Debug.Log("Warning alert #" + (i + 1));
        }
    }
}

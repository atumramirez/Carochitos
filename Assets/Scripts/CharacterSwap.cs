using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CharacterSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;

    public CinemachineCamera cam;

    public int whichCharacter;

    void Start()
    {
        if(character == null && possibleCharacters.Count >= 1)
        {
            character = possibleCharacters[0];
        }

        Swap();
    }
    public void Swap()
    {
        character = possibleCharacters[whichCharacter];
        character.GetComponent<PlayerInput>().enabled = true;

        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<PlayerInput>().enabled = false;
            }
        }

        cam.LookAt = character;
        cam.Follow = character;
    }

    public void SwapToNext()
    {
        if (whichCharacter == 0)
        {
            whichCharacter = possibleCharacters.Count - 1;
        }
        else
        {
            whichCharacter -= 1;
        }

        Swap();
    }
}

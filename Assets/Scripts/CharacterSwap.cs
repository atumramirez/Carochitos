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
        character.GetComponent<PlayerControls>().enabled = true;

        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<PlayerControls>().enabled = false;
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

    public void RemoveCharacter(Transform characterToRemove)
    {
        if (characterToRemove == null)
            return;

        if (possibleCharacters.Contains(characterToRemove))
        {
            int index = possibleCharacters.IndexOf(characterToRemove);

            possibleCharacters.Remove(characterToRemove);

            if (possibleCharacters.Count == 0)
            {
                character = null;
                return;
            }

            if (index <= whichCharacter)
            {
                whichCharacter--;

                if (whichCharacter < 0)
                    whichCharacter = 0;
            }

            whichCharacter = Mathf.Clamp(whichCharacter, 0, possibleCharacters.Count - 1);

            Swap();
        }
    }

    public void AddCharacter(Transform newCharacter)
    {
        if (newCharacter == null)
            return;

        if (!possibleCharacters.Contains(newCharacter))
        {
            possibleCharacters.Add(newCharacter);

            var input = newCharacter.GetComponent<PlayerControls>();
            if (input != null)
                input.enabled = false;
        }
    }
}

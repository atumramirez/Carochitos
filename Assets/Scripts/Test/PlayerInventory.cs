using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> capturedCreatures = new List<string>();

    public void AddCreature(Creature creature)
    {
        capturedCreatures.Add(creature.creatureName);

        Debug.Log("Captured: " + creature.creatureName);
    }
}
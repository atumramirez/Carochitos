using UnityEngine;

public class MeleeModule : ScriptableObject
{
    public Vector3 hitboxSize = new(1, 1, 1);
    public float duration = 0.2f;
    public int damage = 10;
}

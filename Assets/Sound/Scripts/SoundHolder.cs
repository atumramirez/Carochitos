using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    public static SoundHolder Instance;
    public AudioClip MenuPause;
    public AudioClip MenuInteract;
    public AudioClip MenuUnpause;

    public void Awake()
    {
        Instance = this;
    }
}

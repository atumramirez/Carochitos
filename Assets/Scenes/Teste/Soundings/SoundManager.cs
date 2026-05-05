using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource soundFBX;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayClip(AudioClip clip, Transform spawn, float volume)
    {
        AudioSource audioSource = Instantiate(soundFBX, spawn.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }
}

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundFBX;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip, Transform spawn, float volume)
    {
        // Istanciar Clip de Som
        AudioSource audioSource = Instantiate(soundFBX, spawn.position, Quaternion.identity);

        // 
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();


        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }

    public AudioMixer audioMixer;

    public void SetMasterVolume(float lvl)
    {
        audioMixer.SetFloat("MasterVolume", lvl);
    }
    public void SetMusicVolume(float lvl)
    {
        audioMixer.SetFloat("MusicVolume", lvl);
    }
    public void SetSoundVolume(float lvl)
    {
        audioMixer.SetFloat("SoundVolume", lvl);
    }

    public void SetVoiceVolume(float lvl)
    {
        audioMixer.SetFloat("VoiceVolume", lvl);
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
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

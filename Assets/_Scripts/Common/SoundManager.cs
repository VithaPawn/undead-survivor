using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    #region Constants
    private const string US_SOUND_EFFECTS_VOLUME = "UsSoundEffectsVolume";
    #endregion Constants

    #region Variables
    [SerializeField] private List<SoundChannelSO> soundChannels;
    [SerializeField] private FloatVariableSO volume;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        float savedVolume = PlayerPrefs.GetFloat(US_SOUND_EFFECTS_VOLUME, 1f);
        volume.SetValue(Mathf.Clamp01(savedVolume), false);
    }

    private void OnEnable()
    {
        volume.OnChanged += Volume_OnChanged;
        foreach (SoundChannelSO sound in soundChannels)
        {
            sound.OnSoundRequested += Sound_OnSoundRequested;
        }
    }

    private void OnDisable()
    {
        volume.OnChanged -= Volume_OnChanged;
        foreach (SoundChannelSO sound in soundChannels)
        {
            sound.OnSoundRequested -= Sound_OnSoundRequested;
        }
    }

    private void Volume_OnChanged()
    {
        PlayerPrefs.SetFloat(US_SOUND_EFFECTS_VOLUME, volume.GetValue());
        PlayerPrefs.Save();
    }

    private void Sound_OnSoundRequested(AudioClip arg1, Vector3 arg2)
    {
        PlaySound(arg1, arg2);
    }


    private void PlaySound(List<AudioClip> audioClipList, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipList[Random.Range(0, audioClipList.Count)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume.GetValue());
    }
    #endregion Methods
}

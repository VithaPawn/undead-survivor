using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundChannelSO", menuName = "ScriptableObjects/Events/SoundChannelSO")]
public class SoundChannelSO : ScriptableObject {
    [SerializeField] private AudioClip sound;
    public event Action<AudioClip, Vector3> OnSoundRequested;

    public void RaiseEvent(Vector3 playPosition)
    {
        if (OnSoundRequested != null)
        {
            OnSoundRequested.Invoke(sound, playPosition);
        }
        else
        {
            Debug.LogWarning("A sound was requested, but nobody picked it up. " +
                "Check why there is no EffectManager already loaded, " +
                "and make sure it's listening on this sound event channel");
        }
    }
}

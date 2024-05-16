using DatabaseSystem.ScriptableObjects;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {
    [SerializeField] private FloatVariableSO level;
    [SerializeField] private ParticleSystemChannelSO levelupEffect;
    [SerializeField] private SoundChannelSO levelupSound;

    private void OnEnable()
    {
        level.OnChanged += Level_Changed;
    }

    private void OnDisable()
    {
        level.OnChanged -= Level_Changed;
    }

    private void Level_Changed()
    {
        levelupSound.RaiseEvent(transform.position);
        levelupEffect.RaiseEvent(transform);
    }
}

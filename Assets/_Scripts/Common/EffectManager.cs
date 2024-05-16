using DatabaseSystem.ScriptableObjects;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    #region Variables
    [SerializeField] private ParticleSystemChannelSO _enemyExplodeEvent;
    [SerializeField] private ParticleSystemChannelSO _bulletExplodeEvent;
    [SerializeField] private ParticleSystemChannelSO _levelupEvent;
    #endregion Variables

    #region Methods
    private void OnEnable()
    {
        _enemyExplodeEvent.OnParticleSystemRequested += PlayEffect;
        _levelupEvent.OnParticleSystemRequested += PlayEffect;
        _bulletExplodeEvent.OnParticleSystemRequested += PlayEffect;
    }

    private void OnDisable()

    {
        _enemyExplodeEvent.OnParticleSystemRequested -= PlayEffect;
        _levelupEvent.OnParticleSystemRequested -= PlayEffect;
        _bulletExplodeEvent.OnParticleSystemRequested -= PlayEffect;
    }

    private void PlayEffect(ParticleSystem effect, Vector3 position, Transform parent)
    {
        if (parent != null)
        {
            PlayEffectInObject(effect, parent);
        }
        else
        {
            PlayEffectAtPosition(effect, position);
        }
    }

    private void PlayEffectAtPosition(ParticleSystem effect, Vector3 position)
    {
        ParticleSystem newEffect = Instantiate(effect, position, Quaternion.identity);
        newEffect.Play();
    }
    private void PlayEffectInObject(ParticleSystem effect, Transform parentTransform)
    {
        ParticleSystem newEffect = Instantiate(effect);
        if (parentTransform != null) { newEffect.transform.SetParent(parentTransform, false); }
    }
    #endregion Methods

}

using System;
using UnityEngine;

namespace DatabaseSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "ParticleSystemChannelSO", menuName = "ScriptableObjects/Events/ParticleSystemChannelSO")]
    public class ParticleSystemChannelSO : ScriptableObject {
        [SerializeField] private ParticleSystem effect;
        public event Action<ParticleSystem, Vector3, Transform> OnParticleSystemRequested;

        public void RaiseEvent(Vector3 playPosition)
        {
            if (OnParticleSystemRequested != null)
            {
                OnParticleSystemRequested.Invoke(effect, playPosition, null);
            }
            else
            {
                Debug.LogWarning("A ParticleSystem was requested, but nobody picked it up. " +
                    "Check why there is no EffectManager already loaded, " +
                    "and make sure it's listening on this ParticleSystem Event channel");
            }
        }

        public void RaiseEvent(Transform parentTransform)
        {
            if (OnParticleSystemRequested != null)
            {
                OnParticleSystemRequested.Invoke(effect, Vector3.zero, parentTransform);
            }
            else
            {
                Debug.LogWarning("A ParticleSystem was requested, but nobody picked it up. " +
                    "Check why there is no EffectManager already loaded, " +
                    "and make sure it's listening on this ParticleSystem Event channel");
            }
        }
    }
}

using System;
using UnityEngine;

namespace DatabaseSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "ParticleSystemChannelSO", menuName = "ScriptableObjects/Events/ParticleSystemChannelSO")]
    public class ParticleSystemChannelSO : ScriptableObject {
        [SerializeField] private GameObject effectPrefab;
        public event Action<ParticleSystem, Vector3, Transform> OnParticleSystemRequested;

        public void RaiseEvent(Vector3 playPosition)
        {
            RaiseEventInternal(playPosition, null);
        }

        public void RaiseEvent(Transform parentTransform)
        {
            RaiseEventInternal(Vector3.zero, parentTransform);
        }

        private void RaiseEventInternal(Vector3 playPosition, Transform parentTransform)
        {
            if (OnParticleSystemRequested != null)
            {
                ParticleSystem effect = effectPrefab.GetComponent<ParticleSystem>();
                if (effect != null)
                {
                    OnParticleSystemRequested.Invoke(effect, playPosition, parentTransform);
                }
                else
                {
                    Debug.LogWarning("The referenced GameObject does not have a ParticleSystem component.");
                }
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

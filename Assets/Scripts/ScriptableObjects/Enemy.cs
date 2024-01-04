using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DatabaseSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
    public class Enemy : ScriptableObject {
        #region Variables
        public int id;
        public PlayingObjects.Enemy prefab;
        public string enemyName;
        public float damageToPlayer;
        public float health;
        public float speed;
        public float bodyRemainDelay;
        public float value;
        public int score;
        public List<ExpProvidedPoint> expProvidedPoints;
        [System.NonSerialized] private bool isInitialize = false;
        private float totalWeight;
        #endregion Variables

        #region Methods
        private void Initialize()
        {
            if (!isInitialize)
            {
                totalWeight = expProvidedPoints.Sum(item => item.weight);
                isInitialize = true;
            }
        }

        public ExpPoint GetRandomExpPoint()
        {
            Initialize();
            float diceRoll = Random.Range(0, totalWeight);
            foreach (var item in expProvidedPoints)
            {
                if (item.weight >= diceRoll)
                {
                    return item.point;
                }
                diceRoll -= item.weight;
            }
            return null;
        }
        #endregion Methods

        #region ExpPoint
        [System.Serializable]
        public class ExpProvidedPoint {
            public ExpPoint point;
            public float weight;
        }
        #endregion ExpPoint
    }
}

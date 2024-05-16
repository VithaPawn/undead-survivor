using UnityEngine;

namespace DatabaseSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "ExpPoint", menuName = "ScriptableObjects/ExpPoint")]
    public class ExpPoint : ScriptableObject {
        public string title;
        public Coin prefab;
        public Sprite sprite;
        public float point;
    }
}

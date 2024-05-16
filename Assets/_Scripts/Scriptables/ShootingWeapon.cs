using UnityEngine;

namespace DatabaseSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "ShootingWeapon", menuName = "ScriptableObjects/Weapons/ShootingWeapon")]
    public class ShootingWeapon : ScriptableObject {
        public int id;
        public float shootingForce;
        public float shootingBreakTime;
    }
}
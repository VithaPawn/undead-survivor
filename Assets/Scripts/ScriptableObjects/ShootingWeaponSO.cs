using UnityEngine;

[CreateAssetMenu(fileName = "ShootingWeaponSO", menuName = "ScriptableObjects/Weapons/ShootingWeaponSO")]
public class ShootingWeaponSO : ScriptableObject {
    public float shootingForce;
    public float shootingBreakTime;
}

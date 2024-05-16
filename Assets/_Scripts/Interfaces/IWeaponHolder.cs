using UnityEngine;

public interface IWeaponHolder {
    public Transform GetWeaponFollowTransform();

    public Weapon GetCurrentWeapon();

    public void SetCurrentWeapon(Weapon weapon);

    public void ClearCurrentWeapon();

    public bool HasCurrentWeapon();
    public Transform GetWeaponHolderTransform();
}

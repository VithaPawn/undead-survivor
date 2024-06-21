public class AutoShooting : Shooting {
    private void Awake()
    {
        SetupShootingWeapon(shootingWeaponSO);
    }

    private void Update()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            HandleShootingCooldown(Shoot);
        }
    }
}

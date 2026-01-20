using Unity.FPS.Game;
using UnityEngine;

public sealed class SfxController : MonoBehaviour
{
    [Header("Shoot Particle")]
    [SerializeField] private ParticleSystem shootParticle;

    [Header("Fire Point")]
    [SerializeField] private Transform firePoint; // 총구(Muzzle)
    [SerializeField] private WeaponController weaponController;


    private void OnEnable()
    {
        // TODO:
        // GameSignals.WeaponFired += OnWeaponFired;
        if (weaponController != null)
        {
        weaponController.OnShoot += OnWeaponFired;
    }
    }

    private void OnDisable()
    {
        // TODO:
        // GameSignals.WeaponFired -= OnWeaponFired;
        if (weaponController != null)
        {
            weaponController.OnShoot -= OnWeaponFired;
        }
    }

    public void BindWeapon(WeaponController weapon)
    {
        if (weaponController != null)
        {
        weaponController.OnShoot -= OnWeaponFired;
        }

        weaponController = weapon;
        firePoint = weaponController != null ? weaponController.WeaponMuzzle : firePoint;

        if (weaponController != null && isActiveAndEnabled)
        {
            weaponController.OnShoot += OnWeaponFired;
        }
    }

    // =========================
    // Event Handler
    // =========================

    private void OnWeaponFired()
    {
        // TODO:
        // - shootParticle / firePoint null 체크
        // - firePoint 위치/회전 적용
        // - 파티클 재생
        if (!shootParticle || !firePoint) return;

        shootParticle.transform.position = firePoint.position;
        shootParticle.transform.rotation = firePoint.rotation;

        shootParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        shootParticle.Play(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManger : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    public float damage = 20;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public WeaponAmmo ammo;
    WeaponBloom bloom;
    ActionStateManager actions;
    WeaponRecoil recoil;

    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed=20;

    public float enemyKickbackForce = 100;

    public Transform leftHandTarget, leftHandHint;
    WeaponClassManager weaponClass;


    private void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;        
    }

    private void OnEnable() 
    {
        if(weaponClass == null)
        {
            weaponClass = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            audioSource = GetComponent<AudioSource>();
            recoil = GetComponent<WeaponRecoil>();
            recoil.recoilFollowPos = weaponClass.recoilFollowPos;
        }
        weaponClass.SetCurrentWeapon(this);
    }

    private void Update()
    {
        if (ShouldFire()) Fire();
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (IsFireRateExceeded() && IsAmmoAvailable() && !IsReloadingOrSwapping())
        {
            if (IsSemiAutoAndMouseClicked() || IsFullAutoAndMouseHeld())
            {
                return true;
            }
        }
        return false;
    }

    void Fire()
    {
        ResetFireRateTimer();
        DecrementAmmo();
        UpdateBarrelOrientation();
        PlayShootingEffects();
        SpawnBullets();
    }

    bool IsFireRateExceeded()
    {
        return fireRateTimer >= fireRate;
    }

    bool IsAmmoAvailable()
    {
        return ammo.currentAmmo > 0;
    }

    bool IsReloadingOrSwapping()
    {
        return actions.currentState == actions.swapState || actions.currentState == actions.reloadState;
    }

    bool IsSemiAutoAndMouseClicked()
    {
        return semiAuto && Input.GetKeyDown(KeyCode.Mouse0);
    }

    bool IsFullAutoAndMouseHeld()
    {
        return !semiAuto && Input.GetKey(KeyCode.Mouse0);
    }

    void ResetFireRateTimer()
    {
        fireRateTimer = 0;
    }

    void DecrementAmmo()
    {
        ammo.currentAmmo--;
    }

    void UpdateBarrelOrientation()
    {
        barrelPos.LookAt(aim.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
    }

    void PlayShootingEffects()
    {
        TriggerMuzzleFlash();
        PlayGunShotAudio();
        ApplyRecoil();
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }

    void PlayGunShotAudio()
    {
        audioSource.PlayOneShot(gunShot);
    }

    void ApplyRecoil()
    {
        recoil.TriggerRecoil();
    }

    void SpawnBullets()
    {
        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;
            bulletScript.direction = barrelPos.transform.forward;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
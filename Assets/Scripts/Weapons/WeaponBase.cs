﻿using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour {

    public int _ammo;
    public int MaxAmmo;

    public float FireInterval;
    private float _currentInterval;

    public ProjectileBase Projectile;

    public Transform BulletSpawnPossition;

    public delegate void VibrateController(ushort time);
    public static event VibrateController OnVibrateController;

    protected virtual void Awake()
    {
        SetUp();
    }

    protected virtual void OnEnable()
    {
        CanFire = true;
    }

    protected void SetUp()
    {
        _ammo = MaxAmmo;
    }

    protected bool IsFiring;
    protected bool CanFire;

    public virtual void StartFire()
    {
        IsFiring = true;
    }

    public virtual void StartGrip()
    {

    }

    public virtual void StopGrip()
    {

    }

    public void StopFire()
    {
        IsFiring = false;
    }

    protected virtual void Update()
    {
        if (_currentInterval > 0.0f)
        {
            _currentInterval -= Time.deltaTime;
        }

        if (IsFiring)
        {
            if (_currentInterval <= 0.0f)
            {
                Fire();
            }
        }

        if (!IsFiring && _currentInterval < 0.0f)
        {
            _currentInterval = 0.0f;
        }
    }

    protected void Fire()
    {
        if (_ammo > 0)
        {
            FireEffect();
            _ammo--;
            _currentInterval += FireInterval;
            FireProjectile();
            if (_ammo == 0)
            {
                NoAmmoReact();
            }
        }
    }

    protected void DoControllerVibrate(ushort time)
    {
        if (OnVibrateController != null)
        {
            OnVibrateController(time);
        }
    }

    protected abstract void FireEffect();

    protected abstract void NoAmmoReact();

    protected virtual void FireProjectile()
    {
        var proj = Instantiate(Projectile, BulletSpawnPossition.position + transform.rotation * Vector3.forward * 0.1f, transform.rotation) as ProjectileBase;
    }

}

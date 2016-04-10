using UnityEngine;
using System.Collections;
using System;

public class Blaster : WeaponBase
{
    public ParticleSystem LoadParticleA;
    public ParticleSystem LoadParticleB;


    public AudioSource LoadSound;

    protected override void OnEnable()
    {
        base.OnEnable();
        LoadParticleA.Stop();
        LoadParticleB.Stop();

        IsFiring = false;
        CanFire = false;
        _isGripping = false;
    }

    int loadCount = 0;

    protected override void FireEffect()
    {
        DoControllerVibrate(2000);
    }

    void FixedUpdate()
    {
        if (_isGripping && _ammo < MaxAmmo)
        {
            if (loadCount >= 2)
            {
                _ammo++;
                DoControllerVibrate(2000);
                loadCount = 0;
            }
            else
            {
                loadCount++;
            }
        }
    }

    private bool _isGripping = false;

    public override void StartFire()
    {
        base.StartFire();
        StopGrip();
    }

    public override void StartGrip()
    {
        base.StartGrip();
        StopFire();
        _isGripping = true;
        LoadSound.Play();

        LoadParticleA.Play();
        LoadParticleB.Play();

        IsFiring = false;
        CanFire = false;
    }

    public override void StopGrip()
    {
        base.StopGrip();
        _isGripping = false;
        CanFire = true;

        LoadSound.Stop();

        LoadParticleA.Stop();
        LoadParticleB.Stop();
    }

    protected override void NoAmmoReact()
    {
    }
}

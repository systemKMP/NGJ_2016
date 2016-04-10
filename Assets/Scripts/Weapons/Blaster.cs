using UnityEngine;
using System.Collections;
using System;

public class Blaster : WeaponBase
{
    public ParticleSystem LoadParticleA;
    public ParticleSystem LoadParticleB;


    public AudioSource LoadSound;

    protected override void Awake()
    {
        base.Awake();
        LoadParticleA.Stop();
        LoadParticleB.Stop();
    }

    protected override void FireEffect()
    {

    }

    void FixedUpdate()
    {
        if (_isGripping && _ammo < MaxAmmo)
        {
            _ammo++;
        }
    }

    private bool _isGripping = false;

    public override void StartGrip()
    {
        base.StartGrip();
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

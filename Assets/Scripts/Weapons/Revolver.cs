using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Revolver : WeaponBase
{

    public Animator Anim;

    public bool IsLoaded;

    private bool firstCheck = false;

    public AudioSource LoadAudio;
    public AudioSource UnloadAudio;
    public AudioSource ShootAudio;

    public Transform BarrelTrans;

    protected override void Awake()
    {
        IsLoaded = true;
        base.Awake();
    }

    protected override void NoAmmoReact()
    {
        UnloadAudio.Play();
        Anim.SetBool("Loaded", false);
        IsLoaded = false;
        Velocities = new List<float>();
        firstCheck = true;
    }

    public List<float> Velocities;
    public Quaternion PreviousRotation;

    protected override void OnEnable()
    {
        base.OnEnable();
        CanFire = true;
        if (_ammo > 0)
        {
            Anim.SetBool("Loaded", true);
        }
    }

    protected void OnDisable()
    {
        BarrelTrans.localRotation = Quaternion.identity;
        Anim.SetBool("Loaded", true);
    }

    protected override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        //if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        //{
        //    Debug.Log("Fixing");
        //}
        if (!IsLoaded)
        {
            var currentRotation = transform.rotation;

            float currentVelocity = 0.0f;

            if (firstCheck)
            {
                firstCheck = false;
            }
            else
            {
                var fwd = transform.rotation * Vector3.forward;
                var Pfwd = PreviousRotation * Vector3.forward;

                var up = fwd - Pfwd;
                currentVelocity = Vector3.Dot(transform.rotation * Vector3.up, up) / Time.fixedDeltaTime;
                Velocities.Add(currentVelocity);

                if (Velocities.Count >= 5)
                {
                    float avg = Velocities.Average();

                    if (avg > currentVelocity * 10.0f && avg > 5.0f)
                    {
                        LoadGun();
                    }
                }
            }

            if (Velocities.Count > 5)
            {
                Velocities.RemoveAt(0);
            }

            PreviousRotation = currentRotation;
        }
    }

    private void LoadGun()
    {
        DoControllerVibrate(2000);
        LoadAudio.Play();
        _ammo = MaxAmmo;
        IsLoaded = true;
        Anim.SetBool("Loaded", true);
    }

    protected override void FireEffect()
    {
        ShootAudio.Play();
        DoControllerVibrate(2000);
        Anim.SetTrigger("Shoot");
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device Device;

    public AudioSource WeaponChangeSound;

    public WeaponBase CurrentWeapon;

    public List<WeaponBase> Weapons;

    private int _currentWeapon = 0;

    void Awake()
    {
        CurrentWeapon = Weapons[_currentWeapon];
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        WeaponBase.OnVibrateController += VibrateController;
    }

    public void GiveNextGun()
    {
        WeaponChangeSound.Play();

        CurrentWeapon.StopGrip();
        CurrentWeapon.StopFire();

        CurrentWeapon.gameObject.SetActive(false);

        _currentWeapon++;
        if (_currentWeapon == Weapons.Count)
        {
            _currentWeapon = 0;
        }

        for (int i = 0; i < Weapons.Count; i++)
        {
            if (i == _currentWeapon)
            {
                CurrentWeapon = Weapons[i];
                CurrentWeapon.gameObject.SetActive(true);
                break;
            }
        }
    }


    private void VibrateController(ushort time)
    {
        if (Device != null)
        {
            Device.TriggerHapticPulse(time);
        }
    }

    void FixedUpdate()
    {
        if (Device == null)
        {
            Device = SteamVR_Controller.Input((int)trackedObj.index);
        }

        float intensity = Device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

        if (Device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GiveNextGun();
        }

            if (Device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            CurrentWeapon.StartGrip();
        }

        if (Device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            CurrentWeapon.StopGrip();
        }


        if (Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            CurrentWeapon.StartFire();
        }

        if (Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            CurrentWeapon.StopFire();
        }


        if (intensity < 0.05f)
        {
            CurrentWeapon.StopFire();
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device Device;

    public WeaponBase Weapon;

    public List<WeaponBase> Weapons;

    private int _currentWeapon;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        WeaponBase.OnVibrateController += VibrateController;
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



        if (Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Weapon.StartFire();
        }

        if (Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Weapon.StopFire();
        }


        if (intensity < 0.05f)
        {
            Weapon.StopFire();
        }
    }
}

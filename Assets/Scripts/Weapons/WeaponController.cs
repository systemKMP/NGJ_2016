using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device Device;

    public WeaponBase Weapon;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        if (Device == null)
        {
            Device = SteamVR_Controller.Input((int)trackedObj.index);
        }

        if (Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Weapon.StartFire();
        }

        if (Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Weapon.StopFire();

        }
    }
}

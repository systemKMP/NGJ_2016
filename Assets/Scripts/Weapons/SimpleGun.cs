using UnityEngine;
using System.Collections;
using System;

public class SimpleGun : WeaponBase {

    protected override void NoAmmoReact()
    {
        _ammo = MaxAmmo;
    }
}

using UnityEngine;
using System.Collections;

public class SimpleProjectile : ProjectileBase {

    protected override void SetupVelocity()
    {
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController.Velocity;
    }

}

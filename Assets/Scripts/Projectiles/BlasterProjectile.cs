using UnityEngine;
using System.Collections;

public class BlasterProjectile : ProjectileBase {

    public float GrowthSpeed;

    protected override void SetupVelocity()
    {
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController._velocity;
    }

    protected override void Update()
    {
        base.Update();
        transform.localScale += Vector3.one * Time.deltaTime * GrowthSpeed;
    }
}

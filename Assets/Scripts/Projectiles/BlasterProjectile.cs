using UnityEngine;
using System.Collections;

public class BlasterProjectile : ProjectileBase {

    public float GrowthSpeed;

    protected override void SetupVelocity()
    {
<<<<<<< .merge_file_a08972
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController._velocity;
=======
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController.Velocity;
>>>>>>> .merge_file_a05796
    }

    protected override void Update()
    {
        base.Update();
        transform.localScale += Vector3.one * Time.deltaTime * GrowthSpeed;
    }
}

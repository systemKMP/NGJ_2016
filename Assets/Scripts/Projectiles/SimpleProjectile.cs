using UnityEngine;
using System.Collections;

public class SimpleProjectile : ProjectileBase {

    protected override void SetupVelocity()
    {
<<<<<<< .merge_file_a07412
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController._velocity;
=======
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController.Velocity;
>>>>>>> .merge_file_a09368
    }

}

using UnityEngine;
using System.Collections;

public class RevolverProjectile : ProjectileBase
{

    public float SplitInterval;
    public int SplitCount;
    public int SplitAmount;
    public float Spread;

    protected override void Start()
    {
        base.Start();
        if (SplitCount > 0)
        {
            StartCoroutine(Split());
        }
    }

    private IEnumerator Split()
    {
        yield return new WaitForSeconds(SplitInterval);
        for (int i = 0; i < SplitAmount; i++)
        {
            var pr = Instantiate(this, transform.position, transform.rotation * Quaternion.Euler(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), Random.Range(-Spread, Spread))) as RevolverProjectile;
            pr.SplitCount = SplitCount - 1;
        }
    }

    protected override void SetupVelocity()
    {
<<<<<<< .merge_file_a09408
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController._velocity;
=======
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController.Velocity;
>>>>>>> .merge_file_a09580
    }


}

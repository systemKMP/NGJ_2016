using UnityEngine;
using System.Collections;

public class RevolverProjectile : ProjectileBase
{

    public float SplitInterval;
    public int SplitCount;
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
        var pr = Instantiate(this, transform.position, transform.rotation * Quaternion.Euler(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), Random.Range(-Spread, Spread))) as RevolverProjectile;
        pr.SplitCount = SplitCount - 1;
    }

    protected override void SetupVelocity()
    {
        Velocity = transform.rotation * Vector3.forward * StartVelocity + Player.Instance.MovController._velocity;
    }


}

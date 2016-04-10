using UnityEngine;
using System.Collections;
using System;

public class FiringEnemy : EnemyBase
{
    public float RotationSpeed;

    public bool ActivatedFire;
    public bool FiringNow;

    public float MovementSpeedMultiplier;

    public GameObject DeathEffect;

    public GameObject Projectile;

    protected override void Start()
    {
        MovementSpeed *= UnityEngine.Random.Range(0.9f, 1.0f);
        base.Start();
    }

    public int BulletsToFire;
    public float FireInterval;

    protected override void Update()
    {

        if (!ActivatedFire)
        {
            MovementSpeedMultiplier = Mathf.MoveTowards(MovementSpeedMultiplier, 1.0f, Time.deltaTime);

            float rotationSpeedMultiplier = (Player.Instance.transform.position - transform.position).magnitude;
            if (rotationSpeedMultiplier < 6.0f)
            {
                rotationSpeedMultiplier = 0.0f;
            }
            else
            {
                rotationSpeedMultiplier = (rotationSpeedMultiplier - 6.0f) / 50.0f;
            }

            Direction = Vector3.RotateTowards(Direction, (Player.Instance.MovController.Velocity.normalized * 50.0f - transform.position).normalized, Time.deltaTime * RotationSpeed * rotationSpeedMultiplier, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Direction);


            if ((Player.Instance.MovController.Velocity.normalized * 50.0f - transform.position).magnitude < 10.0f)
            {

                ActivateFire();
            }
        }
        else
        {
            MovementSpeedMultiplier = Mathf.MoveTowards(MovementSpeedMultiplier, 0.0f, Time.deltaTime);

            Direction = Vector3.RotateTowards(Direction, (Player.Instance.MovController.Head.transform.position - transform.position).normalized, Time.deltaTime * RotationSpeed, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Direction);

            if (MovementSpeedMultiplier < 0.05f)
            {
                if (!FiringNow)
                {
                    StartCoroutine(FireBullets());
                }
            }

        }

        transform.position += MovementSpeed * Direction.normalized * Time.deltaTime * MovementSpeedMultiplier;

    }

    private IEnumerator FireBullets()
    {
        FiringNow = true;
        for (int i = 0; i < BulletsToFire; i++)
        {
            yield return new WaitForSeconds(FireInterval);
            Instantiate(Projectile, transform.position, transform.rotation);
        }
        FiringNow = false;
        DisableFire();
    }

    protected override float OnDeath()
    {
        var dst = Instantiate(DeathEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(dst, 2.0f);
        return 0.0f;
    }

    protected override void OnSpawn()
    {

    }

    public void ActivateFire()
    {
        ActivatedFire = true;
    }

    public void DisableFire()
    {
        ActivatedFire = false;
    }


}

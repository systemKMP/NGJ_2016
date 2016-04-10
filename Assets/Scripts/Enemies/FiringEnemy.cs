using UnityEngine;
using System.Collections;
using System;

public class FiringEnemy : EnemyBase
{
    private Vector3 _targetOffset;
    public float MaxOffset;

    public float RotationSpeed;

    public bool ActivatedFire;
    public bool FiringNow;

    public float SpeedMultiplier;

    public GameObject DeathEffect;

    public GameObject Projectile;

    public Transform FirePoint;

    protected override void Start()
    {
        base.Start();
        _targetOffset = UnityEngine.Random.rotation * Vector3.forward * UnityEngine.Random.value * MaxOffset;
    }

    public int BulletsToFire;
    public float FireInterval;

    protected override void Update()
    {

        if (!ActivatedFire)
        {
            SpeedMultiplier = Mathf.MoveTowards(SpeedMultiplier, 1.0f, Time.deltaTime);

            float rotationSpeedMultiplier = (Player.Instance.transform.position - transform.position).magnitude;
            if (rotationSpeedMultiplier < 6.0f)
            {
                rotationSpeedMultiplier = 0.0f;
            }
            else
            {
                rotationSpeedMultiplier = (rotationSpeedMultiplier - 6.0f) / 50.0f;
            }

            Direction = Vector3.RotateTowards(Direction, (Player.Instance.MovController.transform.position +  Player.Instance.MovController.Velocity.normalized * 220.0f + _targetOffset - transform.position).normalized, Time.deltaTime * RotationSpeed * rotationSpeedMultiplier, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Direction);

            //Debug.Log((Player.Instance.MovController.transform.position + Player.Instance.MovController.Velocity.normalized * 220.0f + _targetOffset - transform.position).magnitude);

            if ((Player.Instance.MovController.transform.position + Player.Instance.MovController.Velocity.normalized * 220.0f + _targetOffset - transform.position).magnitude < 30.0f)
            {
                ActivateFire();
            }
        }
        else
        {
            SpeedMultiplier = Mathf.MoveTowards(SpeedMultiplier, 0.0f, Time.deltaTime);

            Direction = Vector3.RotateTowards(Direction, (Player.Instance.MovController.Head.transform.position - transform.position).normalized, Time.deltaTime * RotationSpeed * 20.0f, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Direction);

            if (SpeedMultiplier < 0.05f)
            {
                if (!FiringNow)
                {
                    StartCoroutine(FireBullets());
                }
            }

        }

        transform.position += MovementSpeed * Direction.normalized * Time.deltaTime * SpeedMultiplier;

    }

    private IEnumerator FireBullets()
    {
        FiringNow = true;
        for (int i = 0; i < BulletsToFire; i++)
        {
            yield return new WaitForSeconds(FireInterval);
            Instantiate(Projectile, FirePoint.transform.position, Quaternion.LookRotation(Player.Instance.MovController.Head.transform.position + Player.Instance.MovController.Velocity * 0.75f - FirePoint.transform.position));
        }
        FiringNow = false;
        ActivatedFire = false;
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

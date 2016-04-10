using UnityEngine;
using System.Collections;
using System;

public class SimpleEnemy : EnemyBase
{
    public float RotationSpeed;

    public GameObject DeathEffect;

    protected override void Start()
    {
        MovementSpeed *= UnityEngine.Random.Range(0.9f, 1.0f);
        base.Start();
    }

    protected override void Update()
    {
        float rotationSpeedMultiplier = (Player.Instance.transform.position - transform.position).magnitude;
        if (rotationSpeedMultiplier < 5.0f)
        {
            rotationSpeedMultiplier = 0.0f;
        } else
        {
            rotationSpeedMultiplier = (rotationSpeedMultiplier - 5.0f) / 40.0f;
        }

        Direction = Vector3.RotateTowards(Direction, (Player.Instance.MovController.Head.transform.position - transform.position).normalized, Time.deltaTime * RotationSpeed * rotationSpeedMultiplier, Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Direction);
        base.Update();
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
}

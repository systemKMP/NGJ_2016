using UnityEngine;
using System.Collections;

public abstract class ProjectileBase : MonoBehaviour {

    public float StartVelocity;

    protected Vector3 Velocity;

    public int Damage;

    protected virtual void Awake()
    {
        Destroy(gameObject, 7.0f);
    }

    protected virtual void Start()
    {
        SetupVelocity();
    }

    protected virtual void SetupVelocity()
    {
        Velocity = transform.rotation * Vector3.forward * StartVelocity;
    }

    protected virtual void Update()
    {
        transform.position += Velocity * Time.deltaTime;
    }

}

using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour {

    public int StartHealth;
    protected int _currentHealth;

    public float MovementSpeed;

    protected Vector3 Direction;

    protected abstract void OnSpawn();

    /// <summary>
    /// Called on enemy death
    /// </summary>
    /// <returns>Return death time</returns>
    protected abstract float OnDeath();


    protected virtual void Start()
    {
        OnSpawn();
        _currentHealth = StartHealth;
    }

    protected virtual void Update()
    {
        transform.position += MovementSpeed * Direction.normalized * Time.deltaTime; 
    }

    public void Hit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    protected virtual void Kill()
    {
        Destroy(gameObject, OnDeath());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 8)
        {
            Hit(col.gameObject.GetComponent<ProjectileBase>().Damage);
        }
    }


}

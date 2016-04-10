using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public WeaponController WeapController;
    public MovementController MovController;

    public ScreenTint Tint;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    public int Score;

    private static Player _instance;

    void Awake()
    {
        _instance = this;
        Score = 0;
        CurrentHealth = MaxHealth;
    }

    public void Die()
    {

    }

    public int MaxHealth;
    public int CurrentHealth;

    public void TakeDamage()
    {
        CurrentHealth--;
        Tint.StartFadeInWithStay();
    }
}

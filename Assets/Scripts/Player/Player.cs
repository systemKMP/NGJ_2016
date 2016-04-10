using UnityEngine;
using System.Collections;
<<<<<<< .merge_file_a06800
=======
using System;
>>>>>>> .merge_file_a06464

public class Player : MonoBehaviour {

    public WeaponController WeapController;
    public MovementController MovController;

<<<<<<< .merge_file_a06800
=======
    public ScreenTint Tint;

>>>>>>> .merge_file_a06464
    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

<<<<<<< .merge_file_a06800
=======
    public int Score;

>>>>>>> .merge_file_a06464
    private static Player _instance;

    void Awake()
    {
        _instance = this;
<<<<<<< .merge_file_a06800
=======
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
>>>>>>> .merge_file_a06464
    }
}

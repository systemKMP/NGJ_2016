using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    public WeaponController WeapController;
    public MovementController MovController;

    public DeathAndEntry DAE;

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
        DAE.TheEnd();
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }

    public int MaxHealth;
    public int CurrentHealth;

    public void TakeDamage()
    {
        CurrentHealth--;
        Tint.StartFadeInWithStay();
    }
}

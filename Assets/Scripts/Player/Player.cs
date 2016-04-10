using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public MusicController MC;

    public Text ScoreField;
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

    public static int LastScore;

    public int Score;

    private static Player _instance;

    void Awake()
    {
        _instance = this;
        Score = 0;
        ScoreField.text = "SCORE:\n" + LastScore;
        CurrentHealth = MaxHealth;
    }

    public void Die()
    {
        LastScore = Score;
        DAE.TheEnd();
        StartCoroutine(EndGame());
        MC.TargetVolume = 0.0f;
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(0);
    }

    public int MaxHealth;
    public int CurrentHealth;

    public void TakeDamage()
    {
        CurrentHealth--;
        Tint.StartFadeInWithStay();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
}

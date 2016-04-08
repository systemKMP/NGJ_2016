using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public WeaponController WeapController;
    public MovementController MovController;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    private static Player _instance;

    void Awake()
    {
        _instance = this;
    }
}

using UnityEngine;
using System.Collections;

public class PlayerHeadController : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            Player.Instance.TakeDamage();
        }
    }

}

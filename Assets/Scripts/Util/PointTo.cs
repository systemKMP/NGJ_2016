using UnityEngine;
using System.Collections;

public class PointTo : MonoBehaviour {

    public Transform Target;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
	}
}

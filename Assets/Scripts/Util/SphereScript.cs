using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float z = Input.GetAxis("Horizontal")*0.5f;
        float y = Input.GetAxis("Vertical")*0.5f;
        float x = 0;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            x = 1f;
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            x = -1f;
        }
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z+z);
	}
}

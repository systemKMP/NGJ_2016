using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

    public float RotateSpeed = 1;
    public bool bool_rotate = true;
    Quaternion RandomRotation;
    public GameObject go;
    public Vector3 RotateAxis = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        go = gameObject;
        RandomRotation = Random.rotation;
        if (RotateAxis != Vector3.zero)
        {
            RandomRotation = Quaternion.AngleAxis(10f, RotateAxis);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bool_rotate == true)
        {
            transform.Rotate(RandomRotation.eulerAngles * Time.deltaTime * RotateSpeed);
        }
    }

    void FixedUpdate()
    {

    }
}

using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

    public float RotateSpeed = 1;
    public bool bool_rotate = true;
    Quaternion RandomRotation;
    public Vector3 RotateAxis = Vector3.zero;

    void Start()
    {
        RandomRotation = Random.rotation;
        if (RotateAxis != Vector3.zero)
        {
            RandomRotation = Quaternion.AngleAxis(10f, RotateAxis);
        }
    }

    void Update()
    {
        if (bool_rotate)
        {
            transform.Rotate(RandomRotation.eulerAngles * Time.deltaTime * RotateSpeed);
        }
    }
}

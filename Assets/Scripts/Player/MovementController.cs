using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    public Transform Head;
    public Transform MovementBase;

    public Vector3 _velocity;

    public float MaxSpeed;
    public float MaxAcceleration;

    private bool _changeVelocity;

    SteamVR_TrackedObject trackedObj;
    FixedJoint joint;

    SteamVR_Controller.Device Device;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    void Update()
    {


        if (Device != null)
        {
            MovementBase.transform.position += _velocity * Time.deltaTime;

            if (_changeVelocity)
            {
                float intensity = Device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

                Debug.Log(Device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger));

                float accelMultiplier = 1.0f;


                Vector3 direction = (transform.position - Head.position).normalized;

                accelMultiplier += Vector3.Angle(direction, _velocity) / 180.0f;
                _velocity = Vector3.MoveTowards(_velocity, direction * MaxSpeed, Time.deltaTime * MaxAcceleration * intensity * accelMultiplier);
            }
        }
    }

    void FixedUpdate()
    {
        if (Device == null)
        {
            Device = SteamVR_Controller.Input((int)trackedObj.index);

        }

        //if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        //{

        //}
        if (joint == null && Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            _changeVelocity = true;

        }

        if (joint == null && Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            _changeVelocity = false;
        }

        Debug.Log(_changeVelocity);
    }
}

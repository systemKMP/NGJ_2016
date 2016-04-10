using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    public Transform Head;
    public Transform MovementBase;

<<<<<<< .merge_file_a05320
    public Vector3 _velocity;
=======
    public Vector3 Velocity;
>>>>>>> .merge_file_a08604

    public float MaxSpeed;
    public float MaxAcceleration;

    private bool _changeVelocity;

    SteamVR_TrackedObject trackedObj;

    SteamVR_Controller.Device Device;

    public AudioSource WindAudio;

    public float MaxAmplitude;
    public float MinWinSoundSpeed;
    public float MaxWinSoundSpeed;
    public float StartWindPitch;
    public float EndWindPitch;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Device != null)
        {
<<<<<<< .merge_file_a05320
            MovementBase.transform.position += _velocity * Time.deltaTime;
=======
            MovementBase.transform.position += Velocity * Time.deltaTime;
>>>>>>> .merge_file_a08604

            if (_changeVelocity)
            {
                float intensity = Device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

                float accelMultiplier = 1.0f;

                Vector3 direction = (transform.position - Head.position).normalized;

<<<<<<< .merge_file_a05320
                accelMultiplier += Vector3.Angle(direction, _velocity) / 180.0f;
                _velocity = Vector3.MoveTowards(_velocity, direction * MaxSpeed, Time.deltaTime * MaxAcceleration * intensity * accelMultiplier);
=======
                accelMultiplier += Vector3.Angle(direction, Velocity) / 180.0f;
                Velocity = Vector3.MoveTowards(Velocity, direction * MaxSpeed, Time.deltaTime * MaxAcceleration * intensity * accelMultiplier);
>>>>>>> .merge_file_a08604
            }
        }

        float prc = 0.0f;

<<<<<<< .merge_file_a05320
        if (_velocity.magnitude > MinWinSoundSpeed)
        {
            prc = Mathf.Min((_velocity.magnitude - MinWinSoundSpeed) / (MaxWinSoundSpeed - MinWinSoundSpeed), 1.0f);
=======
        if (Velocity.magnitude > MinWinSoundSpeed)
        {
            prc = Mathf.Min((Velocity.magnitude - MinWinSoundSpeed) / (MaxWinSoundSpeed - MinWinSoundSpeed), 1.0f);
>>>>>>> .merge_file_a08604
        }

        WindAudio.volume = prc * MaxAmplitude;
        WindAudio.pitch = prc * (EndWindPitch - StartWindPitch) + StartWindPitch;

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
        if (Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            _changeVelocity = true;

        }

        if (Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            _changeVelocity = false;
        }

    }
}

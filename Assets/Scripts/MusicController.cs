using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{

    public AudioClip Loop;

    public AudioSource Source;

    public float TargetVolume;


    void Update()
    {
        if (TargetVolume == 0.0f)
        {
            Source.volume = Mathf.MoveTowards(Source.volume, TargetVolume, Time.deltaTime * 0.8f);
        }
        else
        {
            Source.volume = Mathf.MoveTowards(Source.volume, TargetVolume, Time.deltaTime * 0.01f);
        }

        if (!Source.isPlaying)
        {
            Source.clip = Loop;
            Source.loop = true;
            Source.Play();
        }
    }
}

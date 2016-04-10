using UnityEngine;
using System.Collections;

public class DeathAndEntry : MonoBehaviour {

    public ScreenTint WhiteTintComponent;

    public AudioSource WhiteNoiseSource;

    public float FadeInSoundTransition, FadeOutSoundTransition;
    public float maxVolume;

    bool fadeInSound, fadeOutSound;
    float fadeInSoundTime, fadeOutSoundTime;

    float currentVolume;

	// Use this for initialization
	void Start () {
        WhiteTintComponent.InitialiseTintComponent();

        TheBeginning();
        // TheEnd();
	}
	
	public void TheBeginning()
    {
        WhiteTintComponent.StartFadeOut();
        StartFadeOutSound();
    }

    public void TheEnd()
    {
        WhiteTintComponent.StartFadeInWithStay();
        StartFadeInSound();
    }

    public void StartFadeInSound()
    {
        currentVolume = WhiteNoiseSource.volume;
        fadeInSound = true;
        WhiteNoiseSource.Play();
    }

    void fadeInSoundAction()
    {
        if (fadeInSound)
        {
            if (fadeInSoundTime < 1)
            {
                fadeInSoundTime += Time.deltaTime / FadeInSoundTransition;
                float newVolume = Mathf.Lerp(currentVolume, maxVolume, fadeInSoundTime);
                WhiteNoiseSource.volume = newVolume;
            } else
            {
                StopFadeInSound();
            }
        }
    }

    void StopFadeInSound() {
        fadeInSound = false;
    }

    public void StartFadeOutSound()
    {
        currentVolume = WhiteNoiseSource.volume;
        fadeOutSound = true;
        WhiteNoiseSource.Play();
    }

    void fadeOutSoundAction()
    {
        if (fadeOutSound)
        {
            if (fadeOutSoundTime < 1)
            {
                fadeOutSoundTime += Time.deltaTime / FadeOutSoundTransition;
                float newVolume = Mathf.Lerp(currentVolume, 0, fadeOutSoundTime);
                WhiteNoiseSource.volume = newVolume;
            }
            else
            {
                StopFadeOutSound();
            }
        }
    }

    void StopFadeOutSound()
    {
        fadeOutSound = false;
        WhiteNoiseSource.Stop();
    }

    void Update()
    {
        fadeInSoundAction();
        fadeOutSoundAction();
    }
}

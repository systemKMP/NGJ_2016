using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTint : MonoBehaviour {

    public float maxAlpha;

    // Time in Seconds
    public float TransitionTime; //Time to Max
    public float StayTime; //How long it stays on
    public float TurnOffTime; //Time to 0

    public bool fadeInTint, fadeOutTint, stayAction, triggerStay, StartFadeInCheck, StartFadeOutCheck;

    bool initialised;

    float timeFadeIn, timeFadeOut, timeStay;

    Image imageComponent;
    float currentAlpha;

    public AudioSource PainSoundSource;

    void Start()
    {
        InitialiseTintComponent();            
    }

    public void InitialiseTintComponent()
    {
        if (!initialised)
        {
            initialised = true;
            fadeInTint = false;
            imageComponent = gameObject.GetComponent<Image>();
        }
    }

    //Trigger a Fade In Action
    public void StartFadeIn()
    {
        fadeOutTint = false;
        currentAlpha = imageComponent.color.a * 255;
        fadeInTint = true;
        timeFadeIn = 0;

        if (PainSoundSource != null)
        {
            PainSoundSource.Play();
        }
    }

    //Trigger a Fade In Action then it will wait X seconds to Fade Out again
    public void StartFadeInWithStay()
    {
        StartFadeIn();
        triggerStay = true;
    }

    void FadeInAction()
    {
        if (fadeInTint)
        {
            if (timeFadeIn < 1)
            {
                timeFadeIn += Time.deltaTime / TransitionTime;
                float newAlpha = Mathf.Lerp(currentAlpha, maxAlpha, timeFadeIn);
                Color newColor = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, newAlpha/255f);
                imageComponent.color = newColor;

                if (PainSoundSource != null)
                {
                    PainSoundSource.volume = newAlpha / 255f;
                }
            }
            else
            {
                StopFadeIn();

                if (triggerStay)
                {
                    SetStay();
                    triggerStay = false;
                }
            }
        }
    }

    void StopFadeIn()
    {
        fadeInTint = false;
    }

    public void StartFadeOut()
    {
        fadeInTint = false;
        currentAlpha = imageComponent.color.a*255;
        fadeOutTint = true;
        timeFadeOut = 0;

        if (PainSoundSource != null)
        {
            PainSoundSource.Play();
        }
    }

    void FadeOutAction()
    {
        if (fadeOutTint)
        {
            if (timeFadeOut < 1)
            {
                timeFadeOut += Time.deltaTime / TurnOffTime;
                float newAlpha = Mathf.Lerp(currentAlpha, 0, timeFadeOut);
                Color newColor = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, newAlpha / 255f);
                imageComponent.color = newColor;

                if (PainSoundSource != null)
                {
                    PainSoundSource.volume = newAlpha / 255f;
                }
            }
            else
            {
                StopFadeOut();
            }
        }
    }

    void StopFadeOut()
    {
        fadeOutTint = false;

        if (PainSoundSource != null)
        {
            PainSoundSource.Stop();
        }
    }

    void CheckStay()
    {
        if (stayAction)
        {
            if (timeStay < Time.time)
            {
                StartFadeOut();
                stayAction = false;
            }
        }
    }

    void SetStay()
    {
        stayAction = true;
        timeStay = Time.time + StayTime;
    }

    void Update () {
        if (StartFadeInCheck)
        {
            StartFadeInCheck = false;
            StartFadeIn();
        }

        if (StartFadeOutCheck)
        {
            StartFadeOutCheck = false;
            StartFadeOut();
        }

        FadeInAction();
        CheckStay();
        FadeOutAction();
	}
	
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTint : MonoBehaviour {

    public float maxAlpha;

    // Time in Seconds
    public float TransitionTime; //Time to Max
    public float StayTime; //How long it stays on
    public float TurnOffTime; //Time to 0

    public bool fadeInTint, fadeOutTint, stayAction, triggerStay;

    float timeFadeIn, timeFadeOut, timeStay;

    Image imageComponent;
    float currentAlpha;

    void Start()
    {
        fadeInTint = false;
        imageComponent = gameObject.GetComponent<Image>();
    }

    //Trigger a Fade In Action
    public void StartFadeIn()
    {
        fadeOutTint = false;
        currentAlpha = imageComponent.color.a * 255;
        fadeInTint = true;
        timeFadeIn = 0;
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
        FadeInAction();
        CheckStay();
        FadeOutAction();
	}
	
}

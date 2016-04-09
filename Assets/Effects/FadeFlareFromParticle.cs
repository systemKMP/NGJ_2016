using UnityEngine;
using System.Collections;

public class FadeFlareFromParticle : MonoBehaviour {

    GameObject lightObject;
    LensFlare lightComponent;
    float lightIntensity;

    public float TimeBeforeFade;

    public float fadeSpeed;
    float currentTime;
    float currentIntensity;
    

    void Start()
    {
        lightObject = gameObject;
        lightComponent = gameObject.GetComponent<LensFlare>();
        lightIntensity = lightComponent.brightness;

        currentTime = 0f;
        currentIntensity = lightComponent.brightness;

        StartCoroutine("StartFade");
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(TimeBeforeFade);
        while (true)
        {
            lightComponent.brightness = Mathf.Lerp(currentIntensity, 0, currentTime);
            print(Time.deltaTime);
            currentTime += fadeSpeed * Time.deltaTime;

            if (currentTime > 1)
            {
                lightObject.SetActive(false);
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}

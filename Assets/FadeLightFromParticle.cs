using UnityEngine;
using System.Collections;

public class FadeLightFromParticle : MonoBehaviour {

    GameObject lightObject;
    Light lightComponent;
    float lightIntensity;

    public float TimeBeforeFade;

    public float fadeSpeed;
    float currentTime;
    float currentIntensity;
    

    void Start()
    {
        lightObject = gameObject;
        lightComponent = gameObject.GetComponent<Light>();
        lightIntensity = lightComponent.intensity;

        currentTime = 0f;
        currentIntensity = lightComponent.intensity;

        StartCoroutine("StartFade");
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(TimeBeforeFade);
        while (true)
        {
            lightComponent.intensity = Mathf.Lerp(currentIntensity, 0, currentTime);
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

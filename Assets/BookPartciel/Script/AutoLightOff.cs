using UnityEngine;
using System.Collections;

public class AutoLightOff : MonoBehaviour 
{
    public bool destroy = true;

    public float duration = 0.2f;

    public float delayTime = 0.1f;    
    
    public float targetValue = 0.0f;

    float startValue = 1.0f;
    float oldValue = 0.0f;
    


    void OnEnable()
    {
        StartCoroutine(LightOffProcess());
    }

    void OnDisable()
    {
        light.intensity = oldValue;
        StopAllCoroutines();        
    }

    IEnumerator LightOffProcess()
    {
        yield return null;

        oldValue = light.intensity;
        float currentValue = startValue;
        float deltaTime = 0.0f;

        while( deltaTime / duration < 1.0f )
        {
            yield return new WaitForSeconds(delayTime);

            deltaTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(currentValue, targetValue, deltaTime / duration);
            currentValue = light.intensity;
        }

        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}

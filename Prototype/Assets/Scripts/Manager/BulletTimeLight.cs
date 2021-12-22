using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class BulletTimeLight : MonoBehaviour
{
    public List<Light2D> allLight;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float desiredIntensity = 0.1f;
    private List<float> previousIntensity = new List<float> { };
    
    // Start is called before the first frame update
    void Awake()
    {
        
        for (int i = 0; i < allLight.Count; i++)
        {
            previousIntensity.Add(allLight[i].intensity);
        }

        
    }

    public void startEffect(bool isFading)
    {
        StopAllCoroutines();
        StartCoroutine(Lerp(isFading));
    }

    IEnumerator Lerp(bool isFading)
    {
        float timeElapsed = 0;
        if (!isFading)
        {
            while (timeElapsed < fadeDuration)
            {

                for (int a = 0; a < allLight.Count; a++)
                {
                    float temp = Mathf.SmoothStep(allLight[a].intensity, desiredIntensity, timeElapsed / fadeDuration);
                    allLight[a].intensity = temp;
                }

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            for (int a = 0; a < allLight.Count; a++)
            {

                allLight[a].intensity = desiredIntensity;
            }

        } else
        {
            while (timeElapsed < fadeDuration)
            {

                for (int a = 0; a < allLight.Count; a++)
                {
                    float temp = Mathf.SmoothStep(allLight[a].intensity, previousIntensity[a], timeElapsed / fadeDuration);
                    allLight[a].intensity = temp;
                }

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            for (int a = 0; a < allLight.Count; a++)
            {

                allLight[a].intensity = previousIntensity[a];
            }

        }
    } 
}

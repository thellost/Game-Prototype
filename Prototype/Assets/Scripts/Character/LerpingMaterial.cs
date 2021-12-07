using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LerpingMaterial : MonoBehaviour
{

    [SerializeField] float fadeDuration = 2f;
    //[SerializeField] float desiredIntensity = 0.1f;
    public Material greyscaleMaterial;
    public Material blurShaderGraph;


    void Start()
    {
        StartCoroutine(fadeIntensity(true));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopAllCoroutines();
            StartCoroutine(Lerp(true));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopAllCoroutines();
            StartCoroutine(Lerp(false));
        }
    }

    IEnumerator fadeIntensity(bool isFading)
    {
        // fade from opaque to transparent
        if (isFading)
        {
            for (float i = fadeDuration; i >= 0; i -= Time.deltaTime)
            {
                
                greyscaleMaterial.SetFloat("Vector1_469f3a6a76f74478a1e87549b60f2bcd", 1f);
                blurShaderGraph.SetFloat("Vector1_7d9cea4f766d472f9675935af83e45e8", 1f);

                yield return new WaitForEndOfFrame();
            }
        }
        // fade from transparent to opaque
        else
        {
            greyscaleMaterial.SetFloat("Vector1_469f3a6a76f74478a1e87549b60f2bcd", 0f);
            blurShaderGraph.SetFloat("Vector1_7d9cea4f766d472f9675935af83e45e8", 0f);
            
            yield return new WaitForEndOfFrame();
        
        }
    }

    IEnumerator Lerp(bool isFading)
    {
        float timeElapsed = 0;
        if (!isFading)
        {
            while (timeElapsed < fadeDuration)
            {
                var val = Mathf.Lerp(1f, 0, timeElapsed / fadeDuration);
                greyscaleMaterial.SetFloat("Vector1_469f3a6a76f74478a1e87549b60f2bcd", val);
                blurShaderGraph.SetFloat("Vector1_7d9cea4f766d472f9675935af83e45e8", val);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            while (timeElapsed < fadeDuration)
            {

                var val = Mathf.Lerp(0, 1f, timeElapsed / fadeDuration);
                greyscaleMaterial.SetFloat("Vector1_469f3a6a76f74478a1e87549b60f2bcd", val);
                blurShaderGraph.SetFloat("Vector1_7d9cea4f766d472f9675935af83e45e8", val);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
    }
}

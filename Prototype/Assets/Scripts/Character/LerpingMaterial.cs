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
    private float val;


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


    IEnumerator Lerp(bool isFading)
    {
        float timeElapsed = 0;
        val = greyscaleMaterial.GetFloat("_Greyscale");
        if (!isFading)
        {
            while (timeElapsed < fadeDuration)
            {
                val = Mathf.SmoothStep(1, 0, timeElapsed / fadeDuration);
                greyscaleMaterial.SetFloat("_Greyscale", val);
                blurShaderGraph.SetFloat("_Saturation", val);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            while (timeElapsed < fadeDuration)
            {
                val = Mathf.SmoothStep(0, 1, timeElapsed / fadeDuration);
                greyscaleMaterial.SetFloat("_Greyscale", val);
                blurShaderGraph.SetFloat("_Saturation", val);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
    }
}

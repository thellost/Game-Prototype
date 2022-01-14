using System.Collections;
using UnityEngine;

public class LerpingMaterial : MonoBehaviour
{

    [SerializeField] float fadeDuration = 2f;
    //[SerializeField] float desiredIntensity = 0.1f;
    public Material greyscaleMaterial;
    public Material blurShaderGraph;
    private float val;

    private void OnLevelWasLoaded(int level)
    {
        
    }
    public void setMaterialEffect(bool isFading)
    {

        StopAllCoroutines();
        StartCoroutine(LerpMaterial(isFading));
    }

    IEnumerator LerpMaterial(bool isFading)
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

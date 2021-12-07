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
    void Start()
    {
        for (int i = 0; i < allLight.Count; i++)
        {
            Debug.Log(previousIntensity);
            previousIntensity.Add(allLight[i].intensity);
        }
        StartCoroutine(fadeIntensity(true));
    }

    // Update is called once per frame
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
        float velocity = 0.0f;
        // fade from opaque to transparent
        if (isFading)
        {
            // loop over second backwards
            for (float i = fadeDuration; i >= 0; i -= Time.deltaTime)
            {

                // set color with i as alpha
                //img.color = new Color(1, 1, 1, i);
                for (int a = 0; a < allLight.Count; a++)
                {
                    float temp = Mathf.SmoothDamp(allLight[a].intensity, desiredIntensity, ref velocity, fadeDuration);
                    allLight[a].intensity = temp;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 2 second
            for (float i = 0; i <= fadeDuration; i += Time.deltaTime)
            {
                for (int a = 0; a < allLight.Count; a++)
                {
                    float temp = Mathf.SmoothDamp(allLight[a].intensity, previousIntensity[a], ref velocity, fadeDuration);
                    allLight[a].intensity = temp;
                }
                yield return new WaitForEndOfFrame();
            }
        }
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
                    allLight[a].intensity = Mathf.Lerp(allLight[a].intensity, desiredIntensity, timeElapsed / fadeDuration);
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
                    allLight[a].intensity = Mathf.Lerp(allLight[a].intensity, previousIntensity[a], timeElapsed / fadeDuration);
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

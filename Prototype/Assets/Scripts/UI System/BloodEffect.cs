using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public static BloodEffect Instance { get; private set; }
    [SerializeField] float duration;
    private float intensity;
    private Image blood;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        blood = GetComponent<Image>();
    }

    // Update is called once per frame
    public void setBlood ()
    {
        StopAllCoroutines();
        StartCoroutine(startFading());
    }

    IEnumerator startFading()
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            float temp = Mathf.Lerp(1, 0, timeElapsed / duration);
            var tempColor = blood.color;
            tempColor.a = temp;
            blood.color = tempColor;
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}

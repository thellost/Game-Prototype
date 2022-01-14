using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablerSynchronize : MonoBehaviour
{
    [SerializeField] GameObject obj;
    // Start is called before the first frame update
    private void OnEnable()
    {
        obj.SetActive(true);
    }

    private void OnDisable()
    {
        obj.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActiveGameObject : MonoBehaviour
{

    [SerializeField] GameObject obj;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        obj.SetActive(true);
        gameObject.SetActive(false);
    }
}

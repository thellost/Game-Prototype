using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Greyscale : MonoBehaviour
{

    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        Color newColor = tilemap.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Parallax : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public bool isTileMap = true;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        if(cam == null) { cam = Camera.main.gameObject; }
        if (isTileMap)
        {
            length = GetComponent<TilemapRenderer>().bounds.size.x;
        }else
        {
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
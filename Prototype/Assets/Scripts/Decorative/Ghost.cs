using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;
    public float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //generate ghostnya
                GameObject currentghost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentghost.transform.localScale = this.transform.localScale;
                currentghost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentghost, 1f);
            }
        }
    }
}

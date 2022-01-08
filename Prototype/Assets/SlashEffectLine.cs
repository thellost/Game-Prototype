using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffectLine : MonoBehaviour
{
    //call from animation event
   private void disableParent()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble <T>
{
    void OnHit(T damage);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPosDestroy : MonoBehaviour
{
    public void DetachAndDestroy()
    {
        transform.parent = null;

        Destroy(gameObject, 1.0f);
    }
}

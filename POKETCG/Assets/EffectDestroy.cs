using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        Destroy(this.gameObject, 0.55f);        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAttractor : MonoBehaviour
{
    public LightOrb lightOrb;

    private void Awake()
    {
        lightOrb = transform.parent.GetComponent<LightOrb>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<LightOrb>()){
            lightOrb.StartAttractingOtherOrb(other.gameObject.GetComponent<LightOrb>());
        }
    }
}

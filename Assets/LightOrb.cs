using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : Collectible
{
    private Color _orbColor;
    public Color OrbColor {
        get {
            return _orbColor;
        }
        set {
            _orbColor = value;
            GetComponent<Light>().color = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<LightOrb>() != null){
            Debug.Log("Other Orb found");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : Collectible
{
    public InkBox inkBox;

    public avaliableColors ink;
    // TODO: Make ink choosable in inspector

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

    private void Awake()
    {
        inkBox = FindObjectOfType<InkBox>();
        SetInk(avaliableColors.RED);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<LightOrb>() != null){
            Debug.Log("Other Orb found");
        }
    }

    public void SetInk(avaliableColors newInk){
        Color newColor = inkBox.colors[newInk.ToString()];
        ink = newInk;
        OrbColor = newColor;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.color = newColor;
    }
}

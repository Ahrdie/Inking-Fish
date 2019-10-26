using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : Collectible
{
    Rigidbody rigidbody;
    public InkBox inkBox;
    public bool attractingToOtherOrb = false;
    private LightOrb otherOrb;
    public float fuseDistance = 0.2f;
    float attractionIncrement = 0.01f;
    float fuseTimeInS = 2.5f;
    float fuseStart;

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
        rigidbody = gameObject.GetComponent<Rigidbody>();
        SetInk(avaliableColors.RED);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<LightOrb>() != null){
            otherOrb = other.gameObject.GetComponent<LightOrb>();

            if (!otherOrb.attractingToOtherOrb)
            {
                attractingToOtherOrb = true;
                fuseStart = Time.time;

            }

            Debug.Log("Other Orb found");
        }
    }

    private void FixedUpdate()
    {
        if (attractingToOtherOrb){
            if((Time.time - fuseStart) < fuseTimeInS ){
                AttractToOtherOrb();
            }else{
                FuseOrbs();
            }
        }
    }

    private void AttractToOtherOrb(){
        Vector3 toOrb = otherOrb.gameObject.transform.position - transform.position;
        attractionIncrement += 0.005f;
        transform.position = Vector3.Lerp(transform.position, otherOrb.gameObject.transform.position, attractionIncrement);
        //rigidbody.AddForce(toOrb * attractionIncrement);
    }

    public void SetInk(avaliableColors newInk){
        Color newColor = inkBox.colors[newInk.ToString()];
        ink = newInk;
        OrbColor = newColor;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.color = newColor;
    }

    private bool IsCloseEnoughToOtherOrb(){
        float distanceToOtherOrb = (otherOrb.gameObject.transform.position - transform.position).magnitude;
        if (distanceToOtherOrb < fuseDistance){
            return true;
        }else{
            return false;
        }

    }

    private void FuseOrbs(){
        SetInk(inkBox.MixColors(ink, otherOrb.ink));
        Destroy(otherOrb.gameObject);
        attractingToOtherOrb = false;
        rigidbody.velocity = Vector3.zero;
    }
}

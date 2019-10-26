﻿using System.Collections;
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
    float fuseTimeInS = 0.5f;
    float fuseStart;

    [ExecuteAlways] public avaliableColors ink;
    // TODO: Make ink live update on change in Editor

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
        SetInk(ink);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<LightOrb>() != null){
            otherOrb = other.gameObject.GetComponent<LightOrb>();

            if (!otherOrb.attractingToOtherOrb)
            {
                StartAttractingOtherOrb();
                otherOrb.StartAttractingOtherOrb(this);
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

    private void StartAttractingOtherOrb(){
        attractingToOtherOrb = true;
        fuseStart = Time.time;
    }

    public void StartAttractingOtherOrb(LightOrb orbToAttract){
        otherOrb = orbToAttract;
        StartAttractingOtherOrb();
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
        Debug.Log("Fuse to " + inkBox.MixColors(ink, otherOrb.ink));
        SetInk(inkBox.MixColors(ink, otherOrb.ink));
        Destroy(otherOrb.gameObject);
        attractingToOtherOrb = false;
        rigidbody.velocity = Vector3.zero;
    }
}
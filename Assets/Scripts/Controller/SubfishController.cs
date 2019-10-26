using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubfishController : MonoBehaviour
{
    public avaliableColors ink;
    InkBox inkBox;
    private bool fallenInLine;
    public float timeToFallInLine = -0.01f;// HACK: to let 
    private GameObject frontFish;

    private void Start()
    {
        GetComponent<HingeJoint>().connectedBody = frontFish.GetComponent<Rigidbody>();
        GetComponent<Collider>().enabled = true;
    }

    //private void FixedUpdate()
    //{
    //    if(!fallenInLine){
    //        if (timeToFallInLine > 0)
    //        {
    //            timeToFallInLine -= Time.fixedDeltaTime;
    //        }else if(frontFish != null)
    //        {
    //            GetComponent<HingeJoint>().connectedBody = frontFish.GetComponent<Rigidbody>();
    //            GetComponent<Collider>().enabled = true;
    //            fallenInLine = true;
    //        }
    //    }
    //}

    public void SetInk(avaliableColors newInk){
        inkBox = FindObjectOfType<InkBox>();
        ink = newInk;
        Color newColor = inkBox.colors[newInk];
        GetComponent<Light>().color = newColor;
        GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", newColor);
    }

    public void SetNewFrontFish(GameObject newFrontFish){
        frontFish = newFrontFish;
        Debug.Log("New Front Fish: " + newFrontFish.name);
    }
}

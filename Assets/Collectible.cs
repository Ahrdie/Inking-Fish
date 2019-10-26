using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Collectible : MonoBehaviour
{
    SphereCollider collider;
    public float triggerRadius = 1.5f;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = triggerRadius;
    }


}

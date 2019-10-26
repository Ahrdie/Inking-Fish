using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Collectible : MonoBehaviour
{
    SphereCollider collider;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }


}

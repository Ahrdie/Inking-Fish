using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float rotationSpeed;
	public Rigidbody rb;
	public Vector3 torque;
    private Collider collider;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        collider = this.GetComponent<Collider>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableFeeding();
        }
        if (Input.GetMouseButtonUp(0)){
            DisableFeeding();
        }
    }

    void FixedUpdate()
    {
    	float rotate = 0;
        rotate = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(Vector3.up, rotate  * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);

    }

    private void EnableFeeding(){
        Debug.Log("Enable Feeding");
    }

    private void DisableFeeding(){
        Debug.Log("Disable Feeding");
    }
 }



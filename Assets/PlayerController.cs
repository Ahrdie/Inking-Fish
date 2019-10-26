using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float rotationSpeed;
	public Rigidbody rb;
	public Vector3 torque;
    // Start is called before the first frame update
    void Start()
    {
       rb = this.GetComponent<Rigidbody>();
    }

 
    void FixedUpdate()
    {
    	float rotate = 0;
        rotate = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(Vector3.up, rotate  * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);

    }

 


 }



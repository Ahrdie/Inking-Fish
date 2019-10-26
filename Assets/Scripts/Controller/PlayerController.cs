using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float rotationSpeed;
	public Rigidbody rb;
    private Collider collider;
    List<avaliableColors> collectedOrbs = new List<avaliableColors>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnableFeeding();
        }
        if (Input.GetKeyUp(KeyCode.F)){
            DisableFeeding();
        }
    }

    void FixedUpdate()
    {
    	float rotate = 0;
        rotate = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(Vector3.up, rotate  * Time.fixedDeltaTime);
        //transform.Translate(transform.forward * speed * Time.fixedDeltaTime);
        rb.velocity = -transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Collectible>()){
            Debug.Log("Collectible found! " + other.gameObject.name);
            LightOrb otherOrb = other.gameObject.GetComponent<LightOrb>();
            if(otherOrb != null){
                collectedOrbs.Add(otherOrb.ink);
                Destroy(other.gameObject);
            }
        }
    }

    private void EnableFeeding(){
        Debug.Log("Enable Feeding");
        collider.isTrigger = true;
    }

    private void DisableFeeding(){
        Debug.Log("Disable Feeding");
        collider.isTrigger = false;
    }
 }



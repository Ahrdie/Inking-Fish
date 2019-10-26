using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float rotationSpeed;
    public GameObject subfishPrefab;
	public Rigidbody rb;
    private Collider collider;
    List<avaliableColors> collectedOrbs = new List<avaliableColors>();
    private GameObject closestSubfish;

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
                AddSubfish(otherOrb.ink);
                Destroy(other.gameObject);
            }
        }
    }

    private void AddSubfish(avaliableColors fishColor)
    {
        GameObject newSubfish;
        if (closestSubfish != null)
        {
            newSubfish = Instantiate(subfishPrefab, closestSubfish.transform.position + transform.right * 0.5f, closestSubfish.transform.rotation);
            newSubfish.GetComponent<SubfishController>().SetNewFrontFish(closestSubfish);
            closestSubfish = newSubfish;
        }
        else{
            newSubfish = Instantiate(subfishPrefab, transform.position + transform.right * 0.8f, transform.rotation);
            newSubfish.GetComponent<SubfishController>().SetNewFrontFish(this.gameObject);
            closestSubfish = newSubfish;
        }

        SubfishController newSubfishController = newSubfish.GetComponent<SubfishController>();
        newSubfishController.SetInk(fishColor);
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



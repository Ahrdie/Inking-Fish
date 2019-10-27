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
    public Animator jawAnimator;
    private GameObject closestSubfish;
    private bool eating = false;
    public bool swimming = false;
    private ObjectionManager objectionManager;
    private List<GameObject> subfishes = new List<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        objectionManager = FindObjectOfType<ObjectionManager>();
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
        if (swimming)
        {
            float rotate = 0;
            rotate = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(Vector3.up, rotate * Time.fixedDeltaTime);
            rb.velocity = -transform.right * speed;
            Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * 0.5f);
        }
    }

    public void EatOrb(GameObject objectToEat){
        if (eating)
        {
            LightOrb otherOrb = objectToEat.GetComponent<LightOrb>();
            collectedOrbs.Add(otherOrb.ink);
            objectionManager.EatColor(otherOrb.ink);
            AddSubfish(otherOrb.ink);
            Destroy(objectToEat);
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
            newSubfish = Instantiate(subfishPrefab, transform.position + transform.right * 1.1f, transform.rotation);
            newSubfish.GetComponent<SubfishController>().SetNewFrontFish(this.gameObject);
            closestSubfish = newSubfish;
        }

        subfishes.Add(newSubfish);
        UpdateSubfishes();
        SubfishController newSubfishController = newSubfish.GetComponent<SubfishController>();
        newSubfishController.SetInk(fishColor);
    }

    private void EnableFeeding(){
        Debug.Log("Enable Feeding");
        eating = true;
        jawAnimator.SetBool("eats", true);
    }

    private void DisableFeeding(){
        Debug.Log("Disable Feeding");
        eating = false;
        jawAnimator.SetBool("eats", false);
    }

    private void UpdateSubfishes(){
        float springforcePerFish = 2.0f;
        for (int i = 0; i < subfishes.Count; i++){
            JointSpring spring = subfishes[i].GetComponent<HingeJoint>().spring;
            spring.spring = springforcePerFish * (subfishes.Count - i);
        }
    }

    public void StartSwimming(){
        swimming = true;
    }
}



using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]  							// 03
public class ThirdPersonController : MonoBehaviour {
	
	public float rotationSpeed = 60.0f;												// 21 15°/s
	public float walkingSpeed = 8.0f;										// 14 8m/s
	public float jumpSpeed = 8.0f;
	public float mouseSensitivity = 10;
	public float airSpeed 	= 5f;
	public float gravity = 9.81f;
	Vector3 externalVelocity;
	
	public bool lockMovement = false;
	
	Vector3 velocity = Vector3.zero;
	
	CharacterController characterController;    							// 01
	
	
	// Use this for initialization
	void Start () {
		
		characterController = this.GetComponent<CharacterController>();   
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float forward = 0;					
		float strafe = 0;						
		float rotate = 0;	
		

		if(!lockMovement)
		{
			
			forward = Input.GetAxis("Vertical");					
			strafe = Input.GetAxis("Strafe");						
				
		}
		rotate = Input.GetAxis("Horizontal") * rotationSpeed;
		Vector3 airVelocity= Vector3.zero;

			// Camera Control		
			if(Input.GetMouseButton(1))			// CameraControl Mouse
			{
				rotate = Input.GetAxis("Mouse X") * mouseSensitivity;
			}
		
			if(characterController.isGrounded)
			{

				
				velocity = this.transform.right * walkingSpeed * strafe + 
					this.transform.forward * walkingSpeed * forward;	// 12			
				
				if(Input.GetButton ("Jump"))
				{
					velocity.y = jumpSpeed;
					SendMessage("Jump",SendMessageOptions.DontRequireReceiver);
				}		
			}
			else
			{
				if(this.transform.parent != null)
				{
					this.transform.parent.SendMessage("CharacterDetached",this.characterController,SendMessageOptions.DontRequireReceiver);
					this.transform.parent = null;
				}
			
				// ÄNderung !!!!!!!!!!!!!!!!!!! Velocity.y aufaddieren
				airVelocity =  Vector3.up * velocity.y + forward * airSpeed * this.transform.forward + strafe * airSpeed * this.transform.right;
			}		
	
		
		
		velocity.y-=gravity * Time.deltaTime;
						
 		characterController.Move((velocity + airVelocity+externalVelocity)  *  Time.deltaTime);			
		transform.Rotate(Vector3.up, rotate  * Time.deltaTime);
		
	
		Vector3.SmoothDamp( externalVelocity, Vector3.zero, ref externalVelocity, 1);
		//Debug.Log (externalVelocity);
	}
	
	
	void AddVelocity(Vector3 velocity)
	{
		externalVelocity = velocity;
	}
	
	
	
	
		

	
	
}

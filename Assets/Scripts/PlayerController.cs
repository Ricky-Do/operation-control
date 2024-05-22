using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour {
 
	[SerializeField] private float movementSpeed;
	[SerializeField] private float jumpForce;
	private float rotationX = 0f;
	private bool isGrounded = false;
	private bool canMove;
	private GameObject playerCamera;
	private Rigidbody rigidBody;
	[SerializeField] LayerMask layerMask;

 
	private void Start(){
		//Hides cursor when playing
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

		//Finds PlayerCamera and assigns to variable at start
		playerCamera = GameObject.Find("PlayerCamera");
		rigidBody = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	private void Update(){

		//Vector to move the player
		Vector3 inputVector = new Vector3(0, 0, 0);

		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		//Rotate the player camera on the X-axis to look up and down
		rotationX -= mouseY;
		rotationX = Mathf.Clamp(rotationX, -90f, 90f);
		playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
		
		transform.Rotate(0, mouseX, 0);

		//Check if the player is touching the ground
		isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

		//Player inputs
		//Forward
		if(Input.GetKey(KeyCode.W)){
			inputVector += transform.forward;
		}
		//Backward
		if(Input.GetKey(KeyCode.S)){
			inputVector -= transform.forward;
		}
		//Left
		if(Input.GetKey(KeyCode.A)){
			inputVector -= transform.right;
		}
		//Right
		if(Input.GetKey(KeyCode.D)){
			inputVector += transform.right;
		}
		//Jump
		if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
			rigidBody.AddForce(Vector3.up * jumpForce);
		}
		//Run
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			movementSpeed *= 1.25f;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			movementSpeed /= 1.25f;
		}
		//Crouch
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			//playerCamera.transform.position -= transform.up;
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
			movementSpeed /= 2;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl)){
			//playerCamera.transform.position += transform.up;
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
			movementSpeed *= 2;
		}

		//Normalize player movements
		inputVector = inputVector.normalized;

		RaycastHit hit;
		canMove = !Physics.Raycast(transform.position, inputVector, out hit, 1f, layerMask);
		if(canMove == true){
			transform.position += inputVector * movementSpeed * Time.deltaTime;
		}
	}
}

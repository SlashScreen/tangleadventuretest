using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playermovement
{
    public class playermovement : MonoBehaviour
    {
		public Rigidbody rb;
		public float walkSpeed = 0;
		public float runSpeed = 0;
		public float jumpHeight = 0;
		public float jumpWindow = 0;
		public float mouseSensitivity = 0;

		private float last_mouse_x;

		private void Start() {
			rb.centerOfMass = new Vector3(0,-1,0);
			last_mouse_x = Input.mousePosition.x;
		}

		private void Update() {

			//vars

			Vector3 vel = new Vector3();
			Vector2 dir = new Vector2();
			float yvel = rb.velocity.y;
			bool isOnGround = false;
			bool jumped = false;

			//process direction

			dir.y = Input.GetAxis("Vertical");
			dir.x = Input.GetAxis("Horizontal");

			//process jump

			RaycastHit hit; //hit
			int layerMask = 1 << 8; //bit shift?
			layerMask = ~layerMask; //???

			if (Physics.Raycast(transform.position + new Vector3(0,-1,0), Vector3.down, out hit, jumpWindow, layerMask)){ //raycast
				isOnGround = true; //if hit, then gorund is hit
			}

			if (isOnGround && Input.GetAxis("Jump") > 0.0){ //if on ground and jump hit, then hit
				yvel = jumpHeight;
				jumped = true;
				Debug.Log("jump");
			}

			//Mouse controls

			//TODO:
			//Only rotate player if the player is moving
			//Allow camera tilt Y (difficult?)
			//Cam collision 
			
			if (Input.mousePosition.x != last_mouse_x){
				transform.Rotate( 0, Input.mousePosition.x - last_mouse_x, 0 , Space.Self);
			}
			last_mouse_x = Input.mousePosition.x;

			//set velocity
			
			if (dir != Vector2.zero || jumped){
				vel = (transform.forward * walkSpeed * dir.y) + (transform.right * walkSpeed * dir.x) + new Vector3(0,yvel,0);
				rb.velocity = vel;
				Debug.Log(vel);
			}

			
		}


    }
}
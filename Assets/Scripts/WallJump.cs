using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 wallNormal;
	private Vector3 playerFoward;

	public float SpeedOffWallOut;
	public float SpeedOffWallUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision col)
	{
		if (col.collider.tag == "Wall") 
		{
			Debug.DrawRay (col.contacts [0].point, col.contacts [0].normal, Color.red, 1.25f);
			//Debug.Log (col.contacts [0].point);

			this.GetComponent<JumpPhysics> ().gravity = 0;

			if (Input.GetButtonDown("Jump"))
			{
				Debug.Log ("WallJump");
				//rb.AddRelativeForce (col.contacts[0].normal*SpeedOffWallOut);
				//rb.AddRelativeForce (Vector3.up*(SpeedOffWallUp));

				//Causes a reflection off the wall and (speed/force) is added to the player

				//getting the new foward
				wallNormal = col.contacts [0].normal; //code to get the normal from a raycast or collision
				playerFoward = transform.forward;

				float dotProduct = Vector3.Dot((playerFoward *-1), wallNormal);
				Vector3 reflection = (2 * dotProduct) * wallNormal - (playerFoward * -1);

				//assigning the new foward
				transform.forward = reflection;

				//adding speed from the jump 
				Rigidbody playRB = this.GetComponent<Rigidbody>();
				playRB.AddForce(transform.forward * SpeedOffWallOut);
				playRB.AddForce(Vector3.up * SpeedOffWallUp);

				transform.rotation = Quaternion.LookRotation (reflection);


			}
		}

	}
}

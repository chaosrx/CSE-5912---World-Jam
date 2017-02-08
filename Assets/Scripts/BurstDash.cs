using UnityEngine;
using System.Collections;

public class BurstDash : MonoBehaviour {

	private Rigidbody rb;
	public PlayerController Player;
	public AIController AI;
	public float DashSpeed;
	public float DashTime;
	private TrailRenderer Trail;
	public bool CanBurstDash;
	public bool IsDashing;

	private Vector3 moveDirection;

	private CharacterController Controller;
	private SoundPlayer SFXPlayer;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Trail = GetComponent<TrailRenderer> ();
		Player = GetComponent<PlayerController> ();
		Controller = GetComponent<CharacterController> ();
		SFXPlayer = GetComponent<SoundPlayer> ();
		CanBurstDash = false;
		IsDashing = false;

	}

	public void Dash () {
		if (CanBurstDash && !IsGrounded ()) {
			Trail.enabled = true;

			rb.velocity = Vector3.zero;
			rb.useGravity = false;

			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			

			float dot = Vector3.Dot (Camera.main.transform.forward, Vector3.up);
			//Vector3 forward = Camera.main.transform.forward - (Vector3.up * dot);
			//transform.rotation = Quaternion.LookRotation (forward.normalized, Vector3.up);

			if (Player == null) {
				AI.BurstDashVector = (AI.moveDirection.normalized * DashSpeed);
			}

			SFXPlayer.PlaySound (1);
			StartCoroutine (StopTrail ());

			CanBurstDash = false;
			IsDashing = true;

			StopCoroutine ("ReEnableGravity");
			StartCoroutine ("ReEnableGravity");
		}
	}

	// Update is called once per frame
	void Update () {

		if (Player != null) {
			if (Input.GetButtonDown ("Fire3") && CanBurstDash && !IsGrounded ()) {
				Trail.enabled = true;

				rb.velocity = Vector3.zero;
				rb.useGravity = false;

				float moveHorizontal = Input.GetAxis ("Horizontal");
				float moveVertical = Input.GetAxis ("Vertical");

				moveDirection = new Vector3 (moveHorizontal, 0.0f, moveVertical);

				float dot = Vector3.Dot (Camera.main.transform.forward, Vector3.up);
				Vector3 forward = Camera.main.transform.forward - (Vector3.up * dot);
				transform.rotation = Quaternion.LookRotation (forward.normalized, Vector3.up);

				if (moveDirection.magnitude > 0.0f) {
					Player.BurstDashVector = (moveDirection * DashSpeed);
				}
				else {
					Player.BurstDashVector = (forward.normalized * DashSpeed);
				}

				Debug.Log ("Dash");
				SFXPlayer.PlaySound (1);
				StartCoroutine (StopTrail ());

				CanBurstDash = false;
				IsDashing = true;

				StopCoroutine ("ReEnableGravity");
				StartCoroutine ("ReEnableGravity");

			}
		}
	}

	IEnumerator StopTrail()
	{
		yield return new WaitForSeconds (DashTime);
		Trail.enabled = false;
	}


	void OnCollisionEnter (Collision col)
	{
		CanBurstDash = true;
		IsDashing = false;

	}

	public bool IsGrounded()
	{
		RaycastHit hit;
		float distance = 1f;
		Vector3 dir = new Vector3(0, -1);

		if(Physics.Raycast(transform.position, dir, out hit, distance))
		{
			return true;

		}
		else
		{
			return false;
		}
	}

	IEnumerator ReEnableGravity()
	{
		yield return new WaitForSeconds (DashTime);
		rb.velocity = Vector3.zero;
		IsDashing = false;
	}

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIController : MonoBehaviour {

	private float distToGround;

	private float VerticalVelocity;
	private Vector3 InternalVelocity;
	private Rigidbody rb;


	public Vector3 moveDirection = Vector3.zero;
	private Vector3 reflection = Vector3.zero;
	public Vector3 BurstDashVector = Vector3.zero;

	public bool CanBurstDash;

	private int FreezeCount;
	public int FreezeCountMax;

	private float dotProduct;

	public float RunSpeed;
	public float MaxSpeed;
	public float RotateSpeed;
	public float gravity = 9.8f;
	private float defaultGravity;
	public float JumpForce = 10.0f;
	public float WallJumpThreshold;
	public float TriangleJumpThreshold;
	public float WallJumpOffForce = 10.0f;
	public float WallJumpUpForce = 10.0f;
	public float TriangleJumpOffForce = 10.0f;
	public float TriangleJumpUpForce = 10.0f;
	public float CurrentSpeed;

	public float BurstDashDampenFactor;

	public bool IsDashing;

	private SoundPlayer SFXPlayer;
	private Slide slide;
	private Stomp stomp;

	Vector3 currentUp = Vector3.up;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		SFXPlayer = GetComponent<SoundPlayer> ();
		slide = GetComponent<Slide> ();
		stomp = GetComponent<Stomp> ();
	}

	public bool IsGrounded()
	{
		RaycastHit hit;
		float distance = 1.1f;
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

	// Used for Physics calculation
	void FixedUpdate () 
	{
        if (GetComponent<BurstDash>().IsDashing == false)
        {
            rb.AddRelativeForce(Physics.gravity * gravity);
        }

		if (jumping) {
			if (IsGrounded ()) {
				rb.AddForce (new Vector3 (0, JumpForce, 0), ForceMode.Impulse);
				rb.AddForce (new Vector3 (0, JumpForce, 0), ForceMode.Impulse);
				SFXPlayer.PlaySound (0);
			} else if (colWall != null) {
				dotProduct = Vector3.Dot ((transform.forward * -1), colWall.contacts [0].normal);
				SFXPlayer.PlaySound (0);

				float angle = Vector3.Angle (transform.forward, colWall.contacts [0].normal);

				reflection = (2 * dotProduct) * colWall.contacts [0].normal - (transform.forward * -1);

				//transform.forward = reflection;

				// moveDirection = transform.forward * WallJumpOffForce;
				moveDirection = colWall.contacts [0].normal.normalized * WallJumpOffForce;
				moveDirection.y = WallJumpUpForce;

				rb.velocity = Vector3.zero;
				rb.useGravity = false;

				rb.velocity = ((moveDirection + BurstDashVector) * Time.deltaTime);

				StopCoroutine ("ReEnableGravity");
				StartCoroutine ("ReEnableGravity");
			}
		}

		if (colWall == null) {
			rb.AddRelativeForce ((moveDirection + BurstDashVector) * RunSpeed * Time.deltaTime);
			CurrentSpeed = rb.velocity.magnitude;
			// print ("AI: MoveForce=" + ((moveDirection + BurstDashVector) * RunSpeed * Time.deltaTime).magnitude + "; Speed=" + CurrentSpeed);
			BurstDashVector *= BurstDashDampenFactor;
		}

		jumping = false;
		colWall = null;
	}

	bool dashBegun = false;
    public void MoveToward (Vector3 tar) {
		Vector3 direction = new Vector3 (tar.x - this.transform.position.x, 0f, tar.z - this.transform.position.z).normalized;

		//transform.LookAt (tar);
		//transform.rotation = Quaternion.Euler (0f, transform.eulerAngles.y, 0f);

		moveDirection = new Vector3 (direction.x * RunSpeed, 0f, direction.z * RunSpeed);
		// rb.AddRelativeForce ((transform.forward * RunSpeed + BurstDashVector) * RunSpeed * Time.deltaTime);
		// rb.AddRelativeForce ((moveDirection + BurstDashVector) * RunSpeed * Time.deltaTime);
		// print (BurstDashVector.magnitude);
	}

	bool walling = false;
	public void MoveFromWall () {
		walling = true;

/*		if (lastNormal != null) {
			print ("WALLIN");
			moveDirection = new Vector3 (lastNormal.x * RunSpeed, 0f, lastNormal.z * RunSpeed);
			Debug.DrawRay (this.transform.position, moveDirection, Color.green);
		}*/
	}

	bool jumping = false;
    public void Jump () {
		jumping = true;
    }

	public void Slide (float aiSlideTime) {
		slide.AttemptSlide (aiSlideTime);
	}

	public void Stomp () {
		stomp.AttemptStomp ();
	}

	void OnCollisionEnter (Collision col) {
		if (!IsGrounded ()) {
			if (col.collider.tag == "Wall") {
				colWall = col;
				print ("WHAM");
				if (walling) {
					Jump ();
				}
			}
		}
	}

	Collision colWall;
	void OnCollisionStay (Collision col)
	{
		if (!IsGrounded()) {
			
			if (col.collider.tag == "Wall") 
			{
				colWall = col;
/*				Debug.DrawRay (col.contacts [0].point, col.contacts [0].normal, Color.green, 1.25f);
				if (Input.GetButton ("Jump")) 
				{
					dotProduct = Vector3.Dot ((transform.forward * -1), col.contacts [0].normal);
					SFXPlayer.PlaySound (0);

					float angle = Vector3.Angle (transform.forward, col.contacts [0].normal);

					//Wall Jump
					if (angle-90 > WallJumpThreshold) 
					{
						
						Debug.Log ("WallJump");

						reflection = (2 * dotProduct) * col.contacts [0].normal - (transform.forward * -1);

						transform.forward = reflection;

						moveDirection = transform.forward * WallJumpOffForce;
						moveDirection.y = WallJumpUpForce;

						rb.velocity = Vector3.zero;
						rb.useGravity = false;

						rb.velocity = ((moveDirection + BurstDashVector) * Time.deltaTime);
						StopCoroutine ("ReEnableGravity");
						StartCoroutine ("ReEnableGravity");
					}
					//Triangle Jump
					else if (angle-90 > TriangleJumpThreshold) 
					{


						Debug.Log ("TriangleJump");

						reflection = (2 * dotProduct) * col.contacts [0].normal - (transform.forward * -1);

						transform.forward = reflection;

						moveDirection = transform.forward * TriangleJumpOffForce;
						moveDirection.y = TriangleJumpUpForce;

						rb.velocity = Vector3.zero;
						rb.useGravity = false;

						rb.velocity = ((moveDirection + BurstDashVector) * Time.deltaTime);

						StopCoroutine ("ReEnableGravity");
						StartCoroutine ("ReEnableGravity");

					}



				}*/
			}
		}

	}

	IEnumerator ReEnableGravity()
	{
		yield return new WaitForSeconds (0.5f);
		rb.useGravity = true;
	}




}

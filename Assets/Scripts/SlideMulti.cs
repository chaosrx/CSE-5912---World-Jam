using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SlideMulti : NetworkBehaviour {

	private Rigidbody rb;
	public PlayerController Player;
	private CapsuleCollider col;
    //[SyncVar]
    private Transform Scaler;
	private float DefaultSize;
	public float SlideDampenFactor;
	public float SlideDampenAmount;
	public bool IsSliding;
    [SyncVar]
    private float xScale, yScale, zScale;


    private SoundPlayer SFXPlayer;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		col = GetComponent<CapsuleCollider> ();
		Scaler = this.GetComponent<Transform> ();
		Player = GetComponent<PlayerController> ();
		SFXPlayer = GetComponent<SoundPlayer> ();

        xScale = 1f;
        yScale = 1f;
        zScale = 1f;

        DefaultSize = col.height;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            Scaler.localScale = new Vector3(xScale, yScale, zScale);
            return;
        }

        if (IsGrounded ()) 
		{
			
			if (Input.GetButton ("Fire2")) {
				SlideDampenAmount = SlideDampenFactor;
				IsSliding = true;

				col.height = 0.1f;
                yScale = 0.5f;
                Scaler.localScale = new Vector3 (xScale, yScale, zScale);
                CmdSetScale(xScale, yScale, zScale);

                //Debug.Log (rb.velocity);
                //SFXPlayer.PlaySound (3);
            } else 
			{
				SlideDampenAmount = 1.0f;
				IsSliding = false;

				col.height = DefaultSize;
                yScale = 1f;
                Scaler.localScale = new Vector3(xScale, yScale, zScale);
                CmdSetScale(xScale, yScale, zScale);
            }
		}
	}

    [Command] public void CmdSetScale(float x, float y, float z)
    {
        xScale = x;
        yScale = y;
        zScale = z;
        Scaler.localScale = new Vector3(xScale, yScale, zScale);
    }

	public bool IsGrounded()
	{
		RaycastHit hit;
		float distance = 2f;
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


}

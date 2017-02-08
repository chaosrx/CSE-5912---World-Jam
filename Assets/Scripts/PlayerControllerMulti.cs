using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class PlayerControllerMulti : NetworkBehaviour
{

    private float distToGround;

    private float VerticalVelocity;
    private Vector3 InternalVelocity;
    private Rigidbody rb;


    private Vector3 moveDirection = Vector3.zero;
    private Vector3 reflection = Vector3.zero;
    public Vector3 BurstDashVector = Vector3.zero;

    public bool CanBurstDash;

    public GameObject camera;

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
    [SyncVar]
    public float CurrentSpeed;
    public float BurstDashDampenFactor;

    //[SyncVar]
    public Animator PlayerAnimator;

    [SyncVar]
    public bool IsDashing;
    [SyncVar]
    public bool IsSliding;

    private SoundPlayer SFXPlayer;

    Vector3 currentUp = Vector3.up;

    [SyncVar]
    public uint PlayerID;
    public SkinnedMeshRenderer SkinnedMeshRender;


    Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow, Color.black, Color.white, Color.magenta };

    bool setColor = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SFXPlayer = GetComponent<SoundPlayer>();

        if (PlayerAnimator == null)
            PlayerAnimator = GetComponentInChildren<Animator>();

        if (SkinnedMeshRender == null)
            SkinnedMeshRender = GetComponentInChildren<SkinnedMeshRenderer>();

        if (isLocalPlayer)
        {
            GameObject cameraController = GameObject.Find("MultipurposeCameraRig");
            //cameraController.GetComponent<PivotBasedCameraRig>().SetTarget(this.transform);
            cameraController.GetComponent<AutoCam>().SetTarget(this.transform);
            cameraController.GetComponent<AutoCam>().SetTargetRigidbody(rb);

            PlayerID = GetComponent<NetworkIdentity>().netId.Value;

            Debug.Log(PlayerID);

            if (!isServer)
                CmdSetPlayerID(PlayerID);
        }

    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 2f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    // Used for Physics calculation
    void FixedUpdate()
    {
        if (!setColor && PlayerID != 0)
        {
            Material r = SkinnedMeshRender.material;
            Material r1 = Instantiate<Material>(r);
            r1.SetColor("_Color", colors[(PlayerID % colors.Length)]);
            //Debug.Log(PlayerID);
            SkinnedMeshRender.material = r1;
            setColor = true;

            TrailRenderer tr = GetComponent<TrailRenderer>();
            Material tm = tr.material;
            Material tm1 = Instantiate<Material>(tm);
            tm1.SetColor("_Color", colors[(PlayerID % colors.Length)]);
            tr.material = r1;
        }

        if (!isLocalPlayer)
        {
            PlayerAnimator.SetFloat("CurrentSpeed", CurrentSpeed);
            PlayerAnimator.SetBool("IsFalling", !IsGrounded());
            PlayerAnimator.SetBool("IsDashing", IsDashing);
            PlayerAnimator.SetBool("IsSliding", IsSliding);
            return;
        }



        if (GetComponent<SlideMulti2>().IsSliding == false)
        {
            //Basic Character movement
            //---------------------------
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            moveDirection = new Vector3(moveHorizontal * RunSpeed, 0.0f, moveVertical * RunSpeed);


            //Calculates the true normal vector of the camera
            //---------------------------
            float dot = Vector3.Dot(Camera.main.transform.forward, Vector3.up);
            Vector3 forward = Camera.main.transform.forward - (Vector3.up * dot);

            if (IsGrounded())
            {
                transform.rotation = Quaternion.LookRotation(forward.normalized, Vector3.up);

                //Basic jump functionality
                //---------------------------
                if (Input.GetButton("Jump"))
                {
                    rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);

                    SFXPlayer.PlaySound(0);
                }
            }
        }
        //Got the calculations, now make it go!
        //---------------------------
        rb.AddRelativeForce((moveDirection + BurstDashVector) * RunSpeed * Time.deltaTime);
        if (GetComponent<BurstDashMulti>().IsDashing == false)
        {
            rb.AddRelativeForce(Physics.gravity * gravity);
        }

        BurstDashVector *= BurstDashDampenFactor;

        CurrentSpeed = rb.velocity.magnitude;
        PlayerAnimator.SetFloat("CurrentSpeed", CurrentSpeed);
        PlayerAnimator.SetBool("IsFalling", !IsGrounded());
        PlayerAnimator.SetBool("IsDashing", (GetComponent<BurstDashMulti>().IsDashing));
        PlayerAnimator.SetBool("IsSliding", (GetComponent<SlideMulti2>().IsSliding));
        IsDashing = GetComponent<BurstDashMulti>().IsDashing;
        IsSliding = GetComponent<SlideMulti2>().IsSliding;
        if (!isServer)
            CmdAnimatoionUpdate(CurrentSpeed, IsDashing, IsSliding);

        if (isServer)
            RpcAnimationUpdate();

    }

    [Command]
    public void CmdSetPlayerID(uint id)
    {
        PlayerID = id;
    }

    [Command]
    public void CmdAnimatoionUpdate(float cs, bool iD, bool iS)
    {
        CurrentSpeed = cs;
        IsDashing = iD;
        IsSliding = iS;
        /**
        PlayerAnimator.SetFloat("CurrentSpeed", CurrentSpeed);
        PlayerAnimator.SetBool("IsFalling", !IsGrounded());
        PlayerAnimator.SetBool("IsDashing", (GetComponent<BurstDashMulti>().IsDashing));
        PlayerAnimator.SetBool("IsSliding", (GetComponent<SlideMulti2>().IsSliding));
        **/

        //NetworkServer.FindLocalObject(GetComponent<NetworkIdentity>().netId).GetComponent<Animator>().
        //NetworkServer.FindLocalObject(GetComponent<NetworkIdentity>().netId).GetComponent<Animator>().SetFloat("CurrentSpeed", CurrentSpeed);
        //NetworkServer.FindLocalObject(GetComponent<NetworkIdentity>().netId).GetComponent<Animator>().SetBool("IsFalling", !IsGrounded());
        //NetworkServer.FindLocalObject(GetComponent<NetworkIdentity>().netId).GetComponent<Animator>().SetBool("IsDashing", (GetComponent<BurstDashMulti>().IsDashing));
        //NetworkServer.FindLocalObject(GetComponent<NetworkIdentity>().netId).GetComponent<Animator>().SetBool("IsSliding", (GetComponent<SlideMulti2>().IsSliding));
    }

    [ClientRpc]
    public void RpcAnimationUpdate()
    {

    }


    void OnCollisionStay(Collision col)
    {
        if (!IsGrounded())
        {

            if (col.collider.tag == "Wall")
            {
                Debug.DrawRay(col.contacts[0].point, col.contacts[0].normal, Color.green, 1.25f);
                if (Input.GetButton("Jump"))
                {
                    /*
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    Vector3 foward = transform.foward;
                    Vector3 analogInput = foward * moveVertical;
                    analogInput += transform.right * moveHorizontal;

                    foward += analogInput * analogScale; // analogScale need to be set as global float
                    foward = foward.normalized;
                    */

                    dotProduct = Vector3.Dot((transform.forward * -1), col.contacts[0].normal);
                    SFXPlayer.PlaySound(0);

                    float angle = Vector3.Angle(transform.forward, col.contacts[0].normal);

                    //Wall Jump
                    if (angle - 90 > WallJumpThreshold)
                    {

                        Debug.Log("WallJump");

                        reflection = (2 * dotProduct) * col.contacts[0].normal - (transform.forward * -1);

                        transform.forward = reflection;

                        moveDirection = transform.forward * WallJumpOffForce;
                        moveDirection.y = WallJumpUpForce;

                        rb.velocity = Vector3.zero;
                        rb.useGravity = false;

                        rb.velocity = ((moveDirection + BurstDashVector) * Time.deltaTime);
                        StopCoroutine("ReEnableGravity");
                        StartCoroutine("ReEnableGravity");
                    }
                    //Triangle Jump
                    else if (angle - 90 > TriangleJumpThreshold)
                    {


                        Debug.Log("TriangleJump");

                        reflection = (2 * dotProduct) * col.contacts[0].normal - (transform.forward * -1);

                        transform.forward = reflection;

                        moveDirection = transform.forward * TriangleJumpOffForce;
                        moveDirection.y = TriangleJumpUpForce;

                        rb.velocity = Vector3.zero;
                        rb.useGravity = false;

                        rb.velocity = ((moveDirection + BurstDashVector) * Time.deltaTime);

                        StopCoroutine("ReEnableGravity");
                        StartCoroutine("ReEnableGravity");

                    }



                }
            }
        }

    }

    IEnumerator ReEnableGravity()
    {
        yield return new WaitForSeconds(0.5f);
        rb.useGravity = true;
    }




}

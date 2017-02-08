using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StompMulti : NetworkBehaviour
{

    private Rigidbody rb;
    public float StompSpeed;

    private SoundPlayer SFXPlayer;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SFXPlayer = GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButtonDown("Fire2") && !IsGrounded())
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.isKinematic = false;
            rb.AddRelativeForce(Vector3.down * StompSpeed);
            SFXPlayer.PlaySound(3);
            Debug.Log("Stomp");
        }
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
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
}

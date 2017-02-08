using UnityEngine;
using System.Collections;

public class DemoPlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(-movement * speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<SpeedWarpCharge>().charge.value == 100)
        {
            GetComponent<SpeedWarpCharge>().charge.value = 0;
            speed *= 2.0f; Invoke("DeactivateSpeedWarp", 5.0f);
        }
    }

    void DeactivateSpeedWarp()
    {
        speed /= 2.0f;
    }
}

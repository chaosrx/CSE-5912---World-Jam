using UnityEngine;
using System.Collections;

public class HoverMotor : MonoBehaviour
{

    public float speed = 90f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;
    private float powerInput;
    private float turnInput;
    private Rigidbody carRigidbody;


    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    //    powerInput = Input.GetAxis("Vertical");
    //    turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

       // Vector3 tmp = new Vector3(0, 0, -turnInput); /* Rotation de la moto quand on tourne */
       // transform.transform.eulerAngles = tmp * 7;

     //   Vector3 rotation = new Vector3(turnInput, 0, 0); /*Translation quand on tourne */
     //   transform.Translate(rotation * 0.5f);

    //    if (transform.position.y <= hoverHeight + 4)
    //        carRigidbody.AddRelativeForce(0f, powerInput * speed, 0f);
   //     carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
    //    carRigidbody.AddRelativeForce(0f, 0f, speed);
    //    speed = ((transform.position.z * 0.3f) + 100);
    }
}
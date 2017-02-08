using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float torqueX;
    public float torqueY;
    public float torqueZ;

    Vector3 actualTorque;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
        actualTorque = new Vector3 (torqueX, torqueY, torqueZ);
		rb.AddTorque (actualTorque, ForceMode.VelocityChange);
		StartCoroutine (SpeedUpdate ());
	}

	IEnumerator SpeedUpdate () {
		while (true) {
			rb.angularVelocity = new Vector3 ();
			rb.AddTorque (actualTorque, ForceMode.VelocityChange);
			yield return new WaitForSeconds (1f);
		}
	}
}

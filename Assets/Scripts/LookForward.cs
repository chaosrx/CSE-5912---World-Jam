using UnityEngine;
using System.Collections;

public class LookForward : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.LookAt (Player.transform, Vector3.up);
	
	}
}

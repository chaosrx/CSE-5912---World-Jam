using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {

	public bool IsOnFloor;
	// Use this for initialization
	void Start () {
		IsOnFloor = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Floor") 
		{
			Debug.Log ("Floor");
			IsOnFloor = true;
		}

	}

	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Floor") 
		{
			IsOnFloor = false;
		}

	}

}

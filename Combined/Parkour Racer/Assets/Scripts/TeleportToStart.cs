using UnityEngine;
using System.Collections;

public class TeleportToStart : MonoBehaviour {

    Vector3 start;

    void Start() {
    	start = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = start;
		}
	}
}
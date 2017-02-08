using UnityEngine;
using System.Collections;

public class PistonMove : MonoBehaviour {

	public float scaleLimit, scaleSpeed;
	float startingScale;
	bool expanding = true;

	void Start () {
		startingScale = this.transform.localScale.y;
	}

	void Update () {
		if (expanding) {
			Expand ();
		} else {
			Contract ();
		}

		CheckExpanding ();
	}

	void Expand () {
		this.transform.localScale += new Vector3 (0f, scaleSpeed, 0f);
		this.transform.Translate (0f, scaleSpeed / 2, 0f);
	}

	void Contract () {
		this.transform.localScale -= new Vector3 (0f, scaleSpeed, 0f);
		this.transform.Translate (0f, -scaleSpeed / 2, 0f);
	}

	void CheckExpanding () {
		float scaleY = this.transform.localScale.y;
		if (scaleY >= scaleLimit) {
			expanding = false;
		} else if (scaleY <= startingScale) {
			expanding = true;
		}
	}
}

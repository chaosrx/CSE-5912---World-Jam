using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {

    public bool movingRight, moving;
	public float moveSpeed, moveWidth;
    [Range(-1.0f, 1.0f)]
    public float startingPosition;
	Vector3 centerPosition;

	// Use this for initialization
	void Start () {
		centerPosition = this.transform.position;
        if (!movingRight) {
            moveSpeed *= -1;
        }
        this.transform.Translate(new Vector3(startingPosition*(moveWidth/2f), 0f, 0f));
    }
	
	// Update is called once per frame
	void Update () {
        if (moving) {
            this.transform.Translate(new Vector3(moveSpeed, 0f, 0f));
            if (Vector3.Distance(this.transform.position, centerPosition) >= moveWidth / 2f) {
                moveSpeed *= -1;
                movingRight = !movingRight;
            }
            startingPosition = (this.transform.position.x - centerPosition.x) / (moveWidth / 2f);
        }
	}
}

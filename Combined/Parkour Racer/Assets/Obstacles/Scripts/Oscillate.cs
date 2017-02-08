using UnityEngine;
using System.Collections;

public class Oscillate : MonoBehaviour {

    Vector3 initialPos;
    float timeCounter = 0;
    public bool horizontal;
    public float speed;
    public float width;
    public float height;



	// Use this for initialization
	void Start () {
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime * speed;

        if (horizontal)
        {
            transform.position = initialPos + new Vector3(Mathf.Cos(timeCounter) * width, 0, Mathf.Sin(timeCounter) * height);
        } else
        {
            transform.position = initialPos + new Vector3(Mathf.Cos(timeCounter) * width, Mathf.Sin(timeCounter) * height, 0);
        }
        
	}
}

using UnityEngine;
using System.Collections;

public class DevCheckpointCheat : MonoBehaviour {

    public Vector3 checkpoint_1;
    public Vector3 checkpoint_2;
    public Vector3 checkpoint_3;
    public Vector3 checkpoint_4;
    public Vector3 checkpoint_5;
    public Vector3 checkpoint_6;
    public Vector3 checkpoint_7;
    public Vector3 checkpoint_8;
    public Vector3 checkpoint_9;
    public Vector3 checkpoint_10;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.position = startPos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = checkpoint_1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = checkpoint_2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = checkpoint_3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = checkpoint_4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            transform.position = checkpoint_5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            transform.position = checkpoint_6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            transform.position = checkpoint_7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            transform.position = checkpoint_8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.position = checkpoint_9;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            transform.position = checkpoint_10;
        }

    }
}

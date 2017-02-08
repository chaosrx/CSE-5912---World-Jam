using UnityEngine;
using System.Collections;

public class TutorialSkip : MonoBehaviour {

    public Vector3 location;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.position = location;
        }
    }
}

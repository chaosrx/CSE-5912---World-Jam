using UnityEngine;
using System.Collections;

public class PlayerRacePosition : MonoBehaviour {

    public GameObject checkpointsParent;
    public int furthestCheckpoint = 0;
    public float distanceToNextCheckpoint = 0;

    Vector3 nextCheckpointPosition;
    private Transform[] checkpoints;

	// Use this for initialization
	void Start () {
        checkpoints = checkpointsParent.GetComponentsInChildren<Transform>();

        //ignore checkpoints[0], it is the parent's transform
        nextCheckpointPosition = checkpoints[1].position;
        distanceToNextCheckpoint = Vector3.Distance(transform.position, nextCheckpointPosition);

        InvokeRepeating("DebugStats", 0f, 5f);
    }
	
	// Update is called once per frame
	void Update () {
        nextCheckpointPosition = checkpoints[furthestCheckpoint + 1].position;
        distanceToNextCheckpoint = Vector3.Distance(transform.position, nextCheckpointPosition);
    }

    void DebugStats ()
    {
        Debug.Log("Next Checkpoint: " + (furthestCheckpoint + 1) + " | Distance: " +  distanceToNextCheckpoint.ToString(".0#"));
    }
}

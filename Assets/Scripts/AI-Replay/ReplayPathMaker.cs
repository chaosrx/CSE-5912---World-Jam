using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ReplayPathMaker : MonoBehaviour {
    ReplayPath rP;
    public ReplayPathFollow rpGhost;
    float timer, timeBetweenNodes, timeOfLastNode;
    bool recordingNodes;

    // Use this for initialization
    void Start () {
        timeBetweenNodes = 0.05f;
        recordingNodes = false;
    }

    IEnumerator CreateNodes () {
        while (recordingNodes) {
            rP.AddNode (this.transform.position);
			timeOfLastNode = Time.time;
            yield return new WaitForSeconds (timeBetweenNodes);
        }
    }

    public void StartRecording () {
        rP = new ReplayPath (timeBetweenNodes);
        recordingNodes = true;
        print ("Recording.");
        StartCoroutine (CreateNodes ());
    }

    public void StopRecording () {
        if (recordingNodes) {
            recordingNodes = false;
			timer += Time.time - timeOfLastNode;
			rP.SetTimer (timer);
            StopCoroutine (CreateNodes ());
            Save ();
        }
    }

    void Save () {
        BinaryFormatter bf = new BinaryFormatter ();
        // AppData/Roaming/......
        FileStream file = File.Create (Application.persistentDataPath + "/replayTest.dat");
        print ("Replay saved at: " + Application.persistentDataPath + "/replayTest.dat");
        bf.Serialize (file, rP);
        file.Close ();
    }

    void Update () {
		timer += Time.deltaTime;

		// Debug
/*        if (Input.GetKeyDown (KeyCode.P)) {
            if (recordingNodes) {
                StopRecording ();
            } else {
                StartRecording ();
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) {
			// ReplayPathFollow ghost = Instantiate (rpGhost) as ReplayPathFollow;
			Instantiate (rpGhost);
        }
*/
    }

}

[Serializable]
class ReplayPath {
    [Serializable]
    public struct PseudoVector3 {
        public float x, y, z;

        public PseudoVector3 (float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

	// ArrayList nodes = new ArrayList ();
	LinkedList<PseudoVector3> nodes = new LinkedList<PseudoVector3> ();
    float timeBetweenNodes;
	float timer;
	public float Timer {
		get { return timer; }
	}

    public float TimeBetweenNodes {
        get { return timeBetweenNodes; }
    }

    public ReplayPath (float timeBetweenNodes) {
        this.timeBetweenNodes = timeBetweenNodes;
    }

	public void SetTimer (float time) {
		timer = time;
	}

    public void AddNode (Vector3 nP) {
        PseudoVector3 nodePosition = new PseudoVector3 (nP.x, nP.y, nP.z);
        nodes.AddFirst (nodePosition);
    }

    public Vector3 GetNode () {
        PseudoVector3 node = (PseudoVector3)nodes.First.Value;
		nodes.RemoveFirst ();
        return new Vector3 (node.x, node.y, node.z);
    }

    public int NodeCount () {
        return nodes.Count;
    }
}
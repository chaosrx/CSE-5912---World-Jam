using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ReplayPathMaker : MonoBehaviour {
    ReplayPath rP;
	PlayerPosition pPosition;
    public ReplayPathFollow rpGhost;
    float timeBetweenNodes, timeOfLastNode;
    bool recordingNodes;

    // Use this for initialization
    void Start () {
        timeBetweenNodes = 0.05f;
        recordingNodes = false;
		pPosition = this.GetComponent<PlayerPosition> ();
    }

	public static float GetRecordTime (int level) {
		string fileName = "/Map" + level + ".dat";
		if (File.Exists (Application.persistentDataPath + fileName)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + fileName, FileMode.Open);
			ReplayPath rP = (ReplayPath)bf.Deserialize (file);
			file.Close ();
			return rP.Timer;
		} else {
			return Mathf.Infinity;
		}
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

    public void StopRecording (float time, int level) {
        if (recordingNodes) {
            recordingNodes = false;
			rP.SetTimer (time);
            StopCoroutine (CreateNodes ());
			if (pPosition.currentLap == pPosition.totalLaps) {
				Save (level);
			}
        }
    }

	// return true if new record
    bool Save (int level) {
		string fileName = "/Map" + level + ".dat";
        BinaryFormatter bf = new BinaryFormatter ();
		// AppData/Roaming/......
		if (File.Exists (Application.persistentDataPath + fileName)) {
			FileStream bestFile = File.Open (Application.persistentDataPath + fileName, FileMode.Open);
			ReplayPath bestPath = (ReplayPath)bf.Deserialize (bestFile);
			bestFile.Close ();

			if (bestPath.Timer > rP.Timer) {
				Overwrite (fileName);
				return true;
			}
		} else {
			Overwrite (fileName);
			return true;
		}

		return false;
    }

	void Overwrite (string fileName) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + fileName);
		print ("Replay saved at: " + Application.persistentDataPath + fileName);
		bf.Serialize (file, rP);
		file.Close ();
	}

    void Update () {

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
		if (nodes.Count == 0) {
			Debug.Log ("Shit's broke");
			return new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}

        PseudoVector3 node = (PseudoVector3)nodes.Last.Value;
		nodes.RemoveLast ();
        return new Vector3 (node.x, node.y, node.z);
    }

    public int NodeCount () {
        return nodes.Count;
    }
}
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ReplayPathFollow : MonoBehaviour {
    ReplayPath rP;
    float timeBetweenNodes, timeSinceLastNode = 0f, moveRate;
    int i = 0;
	int totalNodes;
    Vector3 nextNode;

	public int levelNumber;

    // Use this for initialization
    void Start() {
		Load ();
    }

	// Update is called once per frame
    IEnumerator PathFollow () {
		while (true) {
			if (timeSinceLastNode >= timeBetweenNodes) {
				++i;
				timeSinceLastNode = 0f;
				if (i == totalNodes) {
					Destroy (this.gameObject);
				}
				else {
					nextNode = rP.GetNode ();
					if (nextNode.x == Mathf.Infinity) {
						break;
					}
				}

				moveRate = CalculateMoveRate ();
			}

			Vector3 dir = nextNode - this.transform.position;
			this.transform.Translate (dir.normalized * moveRate);
			timeSinceLastNode += Time.deltaTime;
			yield return null;
		}
    }

    float CalculateMoveRate() {
        float distance = Vector3.Distance(nextNode, this.transform.position);
        float estFrames = timeBetweenNodes / Time.deltaTime;
        return distance / estFrames;
    }

    public void Load() {
		string fileName = "/Map" + levelNumber + ".dat";
		if (File.Exists(Application.persistentDataPath + fileName)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            rP = (ReplayPath)bf.Deserialize(file);
            file.Close();

            timeBetweenNodes = rP.TimeBetweenNodes;
            this.transform.position = rP.GetNode();
            nextNode = rP.GetNode();
            moveRate = CalculateMoveRate();
			totalNodes = rP.NodeCount ();
			StartCoroutine (PathFollow ());
        }
        else {
            print("File not found, destroying Ghost");
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ReplayPathFollow : MonoBehaviour {
    ReplayPath rP;
    float timeBetweenNodes, timeSinceLastNode = 0f, moveRate;
    int i = 0;
    Vector3 nextNode;

    // Use this for initialization
    void Start() {
        Load();
    }

    // Update is called once per frame
    void Update() {
        if (timeSinceLastNode >= timeBetweenNodes) {
            ++i;
            timeSinceLastNode = 0f;
            if (i == rP.NodeCount()) {
                Destroy(this.gameObject);
            }
            else {
                nextNode = rP.GetNode();
            }

            moveRate = CalculateMoveRate();
        }

        Vector3 dir = nextNode - this.transform.position;
        this.transform.Translate(dir.normalized * moveRate);
        timeSinceLastNode += Time.deltaTime;
    }

    float CalculateMoveRate() {
        float distance = Vector3.Distance(nextNode, this.transform.position);
        float estFrames = timeBetweenNodes / Time.deltaTime;
        return distance / estFrames;
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/replayTest.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/replayTest.dat", FileMode.Open);
            rP = (ReplayPath)bf.Deserialize(file);
            file.Close();

            timeBetweenNodes = rP.TimeBetweenNodes;
            this.transform.position = rP.GetNode();
            nextNode = rP.GetNode();
            moveRate = CalculateMoveRate();
        }
        else {
            print("File not found, destroying Ghost");
            Destroy(this.gameObject);
        }
    }
}

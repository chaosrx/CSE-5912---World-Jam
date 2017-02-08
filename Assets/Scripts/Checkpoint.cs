using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    bool checkpointPassed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!checkpointPassed)
            {
                Debug.Log("Player has passed Checkpoint " + (this.transform.GetSiblingIndex() + 1));
                other.gameObject.GetComponent<PlayerRacePosition>().furthestCheckpoint = this.transform.GetSiblingIndex() + 1;
                checkpointPassed = true;
            }
        }
    }

}

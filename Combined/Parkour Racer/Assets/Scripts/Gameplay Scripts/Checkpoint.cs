using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerPosition>() != null)
            {
                other.transform.GetComponent<PlayerPosition>().updateCheckpoint(this.transform.GetSiblingIndex());
            }
            else
            {
                other.transform.GetComponent<PlayerPositionMulti>().updateCheckpoint(this.transform.GetSiblingIndex());
            }
        }
    }

}

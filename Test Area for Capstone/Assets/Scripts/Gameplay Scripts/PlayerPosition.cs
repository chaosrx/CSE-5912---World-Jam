using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour {

    public int currentCheckpoint;
    public int currentPosition;

    public Text placeText;
    
    private GameObject[] players;
    private GameObject[] checkpoints;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        checkpoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<Checkpoints>().checkpoints;
	}
	
	// Update is called once per frame
	void Update () {
        currentPosition = GetPlayerPosition(players);

        if (currentPosition == 1)
            placeText.text = "1st";
        if (currentPosition == 2)
            placeText.text = "2nd";
        if (currentPosition == 3)
            placeText.text = "3rd";
        if (currentPosition == 4)
            placeText.text = "4th";
    }

    public float DistanceCalc (GameObject player, GameObject checkpoint) {
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = player.transform.position.z;

        float a = checkpoint.transform.up.x;
        float b = checkpoint.transform.up.y;
        float c = checkpoint.transform.up.z;

        float d = -Vector3.Dot(checkpoint.transform.up, checkpoint.transform.position);

        float distance = Mathf.Abs(a * x + b * y + c * z + d) / Mathf.Sqrt(a * a + b * b + c * c);

        return distance;
    }

    public int GetPlayerPosition (GameObject[] players)
    {
        currentPosition = 1;

        for (int n = 0; n < players.Length; n++)
        {
            int rivalCheckpoint = players[n].GetComponent<PlayerPosition>().currentCheckpoint;
            if (currentCheckpoint < rivalCheckpoint)
                currentPosition++;
            else
            {
                float currentDistance = DistanceCalc(this.gameObject, checkpoints[currentCheckpoint]);
                float rivalDistance = DistanceCalc(players[n], checkpoints[rivalCheckpoint]);

                if (currentDistance > rivalDistance)
                    currentPosition++;
            }
        }
        return currentPosition;
    }

    void onTriggerEnter(Collider other)
    {
        currentCheckpoint = System.Convert.ToInt32(other.gameObject.tag);
    }
}

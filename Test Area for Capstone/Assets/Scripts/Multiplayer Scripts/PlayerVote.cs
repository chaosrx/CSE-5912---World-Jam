using UnityEngine;
using System.Collections;

public class PlayerVote : MonoBehaviour {

    private int priorVote;
    private Canvas canvas;

	// Use this for initialization
	void Start () {
        priorVote = -1;
        canvas = FindObjectOfType<Canvas>();

    }
	
	// Update is called once per frame
	public void vote (int mapNumber) {
        if (priorVote == -1)
        {
            canvas.GetComponent<VoteTally>().voteTally[mapNumber-1]++;
        }
        else
        {
            canvas.GetComponent<VoteTally>().voteTally[priorVote-1]--;
            canvas.GetComponent<VoteTally>().voteTally[mapNumber-1]++;
        }
        priorVote = mapNumber;
    }
}

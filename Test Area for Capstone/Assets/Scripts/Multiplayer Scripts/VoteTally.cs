using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoteTally : MonoBehaviour {

    public int[] voteTally;
    public GameObject[] votes;

    void Update()
    {
        for (int n = 0; n < votes.Length; n++)
        {
            if (voteTally[n] == 0)
                votes[n].SetActive(false);
            else
            {
                votes[n].SetActive(true);
                votes[n].GetComponentInChildren<Text>().text = "Votes: " + voteTally[n];
            }
        }
    }
}

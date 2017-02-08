using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class VoteTally : NetworkBehaviour {


    //public SyncListInt vT = new SyncListInt();
    
    public int[] voteTally;
    public GameObject[] votes;

    [SyncVar]
    public int voteTally1;
    [SyncVar]
    public int voteTally2;
    [SyncVar]
    public int voteTally3;
    [SyncVar]
    public int voteTally4;
    [SyncVar]
    public int voteTally5;
    [SyncVar]
    public int voteTally6;
    [SyncVar]
    public int voteTally7;
    [SyncVar]
    public int voteTally8;


    void Update()
    {
        voteTally[0] = voteTally1;
        voteTally[1] = voteTally2;
        voteTally[2] = voteTally3;
        voteTally[3] = voteTally4;

        voteTally[4] = voteTally5;
        voteTally[5] = voteTally6;
        voteTally[6] = voteTally7;
        voteTally[7] = voteTally8;


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

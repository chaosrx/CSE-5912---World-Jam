using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCount : MonoBehaviour {

    public Text playerCount;

	// Update is called once per frame
	void Update () {
        int numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;

        playerCount.text = "Player In Lobby: " + numberOfPlayers;
	}
}

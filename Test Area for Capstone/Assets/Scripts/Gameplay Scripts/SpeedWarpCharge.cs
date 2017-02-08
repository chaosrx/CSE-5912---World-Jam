using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedWarpCharge : MonoBehaviour {

    public Text percent;
    public Slider charge;

    private float elapsedTime;
    private GameObject firstPlace;
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;

	    if (charge.value < 100)
        {
            if (elapsedTime > 1)
            {
                /*
                GameObject[] players = GameObject.FindGameObjectsWithTag("Players");
                foreach (GameObject player in players)
                    if (player.GetComponent<PlayerPosition>().currentPosition == 1)
                        firstPlace = player;
                 */
                
                int currentPosition = this.gameObject.GetComponent<PlayerPosition>().currentPosition;

                if (currentPosition == 1)
                    charge.value += 1;
                if (currentPosition == 2)
                    charge.value += 2;
                if (currentPosition == 3)
                    charge.value += 3;
                if (currentPosition == 4)
                    charge.value += 4;

                percent.text = charge.value + "%";

                elapsedTime = 0.0f;
            }
        }
	}
}

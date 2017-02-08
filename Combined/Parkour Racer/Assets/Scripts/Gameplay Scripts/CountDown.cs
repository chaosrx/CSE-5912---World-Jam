using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CountDown : NetworkBehaviour
{

    public GameObject player;
    public GameObject raceOverlay;

    [SyncVar]
    private float totalTime;
    private Text count;

    void Start()
    {
        count = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {


        // if (player.GetComponent<PlayerPosition>().totalLaps > 1 && totalTime < 2.5f)
        //{
        // count.text = player.GetComponent<PlayerPosition>().totalLaps + " Laps";
        //}

        totalTime += Time.deltaTime;
        if (totalTime > 3)
            count.text = "3";
        if (totalTime > 4)
            count.text = "2";
        if (totalTime > 5)
            count.text = "1";
        if (totalTime > 6)
        {
            count.text = "";
            raceOverlay.GetComponent<StopWatch>().enabled = true;
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");
            if (player.GetComponent<PlayerController>() != null)
            {
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<HoverMotor>().enabled = true;
            }
            else
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject p in players)
                {
                    p.GetComponent<PlayerControllerMulti>().enabled = true;
                    p.GetComponent<HoverMotor>().enabled = true;
                    p.GetComponent<SpeedWarpCharge>().enabled = true;
                }
            }

            this.GetComponent<CountDown>().enabled = false;
            //player.GetComponent<Slide>().enabled = true;
            //player.GetComponent<SpeedWarpCharge>().enabled = true;
        }

    }
}

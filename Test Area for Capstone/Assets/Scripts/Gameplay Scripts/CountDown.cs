using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    public GameObject player;
    public GameObject raceOverlay;

    private float totalTime;

	// Update is called once per frame
	void Update () {
        Text count = GetComponent<Text>();

        totalTime += Time.deltaTime;

        if (totalTime > 3)
            count.text = "3";
        if (totalTime > 4)
            count.text = "2";
        if (totalTime > 5)
            count.text = "1";
        if (totalTime > 6)
        {
            raceOverlay.GetComponent<StopWatch>().enabled = true;
            player.GetComponent<DemoPlayerController>().enabled = true;
            player.GetComponent<SpeedWarpCharge>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}

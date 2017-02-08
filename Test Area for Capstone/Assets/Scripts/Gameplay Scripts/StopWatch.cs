using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour {

    public Text time;
    public float totalTime;

	// Update is called once per frame
	void Update () {
        totalTime += Time.deltaTime;
        int intTime = (int)(totalTime);
        int seconds = (int)(intTime % 60);
        int minutes = (int)(intTime / 60);
        int milsecs = (int)((totalTime - (int)intTime) * 100);
 
        if (milsecs < 10 && seconds < 10)
        {
            time.text = minutes + ":0" + seconds + ":0" + milsecs;
        }
        else if (milsecs < 10)
        {
            time.text = minutes + ":" + seconds + ":0" + milsecs;
        }
        else if (seconds < 10)
        {
            time.text = minutes + ":0" + seconds + ":" + milsecs;
        }
        else
        {
            time.text = minutes + ":" + seconds + ":" + milsecs;
        }
    }
}

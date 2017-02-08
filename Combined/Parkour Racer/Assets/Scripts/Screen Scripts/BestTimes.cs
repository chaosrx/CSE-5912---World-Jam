using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BestTimes : MonoBehaviour {

    public Text[] bestTimes;

    private string[] lines;
    private static int defaultTime;

    // Use this for initialization
    void Start () {

        
        for (int n = 0; n < 4; n++)
        {
			float recordTime = ReplayPathMaker.GetRecordTime (n);
			int totalTime;
			if (recordTime == Mathf.Infinity) {
				totalTime = -1;
			} else {
				totalTime = System.Convert.ToInt32 (recordTime);
			}

            if (n == 0 || totalTime == -1)
            {
                bestTimes[n].text = "Best Time:    N/A";
            }
            else
            {
                int milsecs = totalTime % 100;
                int seconds = (totalTime / 100) % 60;
                int minutes = (totalTime / 100) / 60;

                if (milsecs < 10 && seconds < 10)
                {
                    bestTimes[n].text = "Best Time:    " + minutes + ":0" + seconds + ":0" + milsecs;
                }
                else if (milsecs < 10)
                {
                    bestTimes[n].text = "Best Time:    " + minutes + ":" + seconds + ":0" + milsecs;
                }
                else if (seconds < 10)
                {
                    bestTimes[n].text = "Best Time:    " + minutes + ":0" + seconds + ":" + milsecs;
                }
                else
                {
                    bestTimes[n].text = "Best Time:    " + minutes + ":" + seconds + ":" + milsecs;
                }
            }
        }
    }
}

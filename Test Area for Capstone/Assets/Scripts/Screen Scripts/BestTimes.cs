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
        lines = File.ReadAllLines(Application.dataPath + "/recordTimes.txt");
        defaultTime = 888888;

        for (int n = 0; n < bestTimes.Length; n++)
        {
            int totalTime = System.Convert.ToInt32(lines[n]);

            if (totalTime == defaultTime)
            {
                bestTimes[n].text = "Best Time:    88:88:88";
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

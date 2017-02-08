using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    public GameObject player;
    public GameObject raceOverlay;
    public GameObject postOverlay;

    public Text time;
    public Text place;
    public Text results;

    public int mapNumber;

    private float totalTime;
    private float recordTime;

    private string[] lines;

    void Start()
    {
        lines = File.ReadAllLines(Application.dataPath + "/recordTimes.txt");
        recordTime = (float)System.Convert.ToDouble(lines[mapNumber - 1]);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            totalTime = Mathf.Floor(raceOverlay.GetComponent<StopWatch>().totalTime * 100);
            raceOverlay.GetComponent<StopWatch>().enabled = false;

            if (totalTime < recordTime)
            {
                Invoke("CleanUp", 2.0f);
                Invoke("NewRecord", 2.0f);
            }
            else
            {
                Invoke("CleanUp", 2.0f);
                Invoke("ShowCredits", 2.0f);
            }
        }
    }

    void CleanUp()
    {
        player.gameObject.SetActive(false);
        raceOverlay.SetActive(false);
        postOverlay.SetActive(true);
    }

    void ShowCredits()
    {
        results.fontSize = 50;
        results.text = place.text + "   " + time.text;
    }

    void NewRecord()
    {
        results.fontSize = 45;
        results.text = place.text + "   " + time.text + "   New Record!";
        lines[mapNumber - 1] = totalTime.ToString();
        File.WriteAllLines(Application.dataPath + "/recordTimes.txt", lines);
    }
}

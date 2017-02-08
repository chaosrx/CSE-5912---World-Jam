using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour {

    private GameObject raceOverlay;
    private GameObject postOverlay;

    private Text results;

    public int mapNumber;
    // 1 = Tutorial, 2 = Power, 3 = 

    private float totalTime;
    private float recordTime;

    // private string[] lines;

    public int totalLaps;
    public int currentLap = 1;

    private int currentCheckpoint = -1;
    public int currentPosition;

    private Text timeText;

    private GameObject[] players;
    private GameObject checkpoints;
    private int numCheckpoints;

    // Use this for initialization
    void Start () {
        timeText = GameObject.FindGameObjectWithTag("Time Text").GetComponent<Text>();
        raceOverlay = GameObject.FindGameObjectWithTag("Race Overlay");
        postOverlay = GameObject.FindGameObjectWithTag("Post Overlay");   
        results = postOverlay.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        //results = GameObject.FindGameObjectWithTag("Results").GetComponent<Text>();

        if (totalLaps == 0) { totalLaps = 1; };

        checkpoints = GameObject.FindGameObjectWithTag("Checkpoints");
        numCheckpoints = checkpoints.transform.childCount;
        /*lines = File.ReadAllLines(Application.dataPath + "/recordTimes.txt");
        recordTime = (float)System.Convert.ToDouble(lines[mapNumber - 1]);*/

        this.GetComponent<ReplayPathMaker>().StartRecording();
		recordTime = ReplayPathMaker.GetRecordTime (mapNumber);

        postOverlay.SetActive(false);
    }
	

    float DistanceCalc (GameObject player, GameObject checkpoint) {
        //Only calculate based on X and Z distance, not Y (height)
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        float checkpointX = checkpoint.transform.position.x;
        float checkpointZ = checkpoint.transform.position.z;

        return Mathf.Sqrt(Mathf.Pow(playerX - checkpointX, 2f) + Mathf.Pow(playerZ - checkpointZ, 2f));
    }

    public void updateCheckpoint(int newCheckpoint)
    {
        if (newCheckpoint == numCheckpoints - 1)
        {
            if (currentLap == totalLaps)
            {
                totalTime = Mathf.Floor(raceOverlay.GetComponent<StopWatch>().totalTime * 100);

                raceOverlay.GetComponent<StopWatch>().enabled = false;
                this.GetComponent<PlayerController>().enabled = false;
                this.GetComponent<Slide>().enabled = false;
                this.GetComponent<HoverMotor>().enabled = false;

                if (totalTime < recordTime)
                {
                    //BEST TIME
                    this.GetComponent<ReplayPathMaker>().StopRecording(totalTime, mapNumber);
                    CleanUp();
                    NewRecord();
                }
                else
                {
                    //NOT BEST
                    this.GetComponent<ReplayPathMaker>().StopRecording(totalTime, mapNumber);
                    CleanUp();
                    ShowCredits();
                }
            } else
            {
                Debug.Log(currentLap);
                Debug.Log(totalLaps);
                Debug.Log("Player completed a lap");

                currentLap++;
                currentCheckpoint = -1;
            }
        } else
        {
            Debug.Log("Player has passed checkpoint " + newCheckpoint);
            currentCheckpoint = newCheckpoint;
        }   
    }

    void CleanUp()
    {
        raceOverlay.SetActive(false);
        postOverlay.SetActive(true);
        
    }

    void ShowCredits()
    {
        results.fontSize = 50;
        results.text = timeText.text;
    }

    void NewRecord()
    {
        results.fontSize = 45;
        results.text = timeText.text + "   New Record!";
        /*lines[mapNumber - 1] = totalTime.ToString();
        File.WriteAllLines(Application.dataPath + "/recordTimes.txt", lines);*/
    }
}

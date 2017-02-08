using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PlayerPositionMulti : NetworkBehaviour
{

    private GameObject raceOverlay;
    private GameObject postOverlay;

    private Text results;

    public int mapNumber;
    // 1 = Tutorial, 2 = Power, 3 = 

    private float totalTime;
    private float recordTime;

    private string[] lines;

    public int totalLaps;
    public int currentLap = 1;

    public int currentCheckpoint = -1;
    public int currentPosition;

    private Text timeText;
    private Text placeText;

    private GameObject[] players;
    private GameObject checkpoints;
    private int numCheckpoints;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("PowerMulti"))
            totalLaps = 3;

        players = GameObject.FindGameObjectsWithTag("Player");
        checkpoints = GameObject.FindGameObjectWithTag("Checkpoints");
        numCheckpoints = checkpoints.transform.childCount;

        if (!isLocalPlayer)
            return;

        postOverlay = GameObject.Find("Post Overlay");

        results = postOverlay.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        Debug.Log("Test");
        results = GameObject.FindGameObjectWithTag("Results").GetComponent<Text>();

        postOverlay.SetActive(false);

        timeText = GameObject.FindGameObjectWithTag("Time Text").GetComponent<Text>();
        raceOverlay = GameObject.Find("Race Overlay");

        if (GameObject.FindGameObjectWithTag("Place Text") == null)
        {
            this.GetComponent<PlayerPositionMulti>().enabled = false;
            return;
        }

        placeText = GameObject.FindGameObjectWithTag("Place Text").GetComponent<Text>();



        if (totalLaps == 0) { totalLaps = 1; };

        players = GameObject.FindGameObjectsWithTag("Player");

        checkpoints = GameObject.FindGameObjectWithTag("Checkpoints");
        numCheckpoints = checkpoints.transform.childCount;
        lines = File.ReadAllLines(Application.dataPath + "/recordTimes.txt");
        //recordTime = 888888f;
        if (mapNumber > 0)
            recordTime = (float)System.Convert.ToDouble(lines[mapNumber - 1]);
        else
            recordTime = 888888f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            //currentPosition = GetPlayerPosition();
            return;
        }

        currentPosition = GetPlayerPosition();

        if (currentPosition == 1)
            placeText.text = "1st";
        if (currentPosition == 2)
            placeText.text = "2nd";
        if (currentPosition == 3)
            placeText.text = "3rd";
        if (currentPosition == 4)
            placeText.text = "4th";

        //placeText.text = placeText.text + " " + currentCheckpoint + "/" + numCheckpoints ;
    }

    float DistanceCalc(GameObject player, GameObject checkpoint)
    {
        //Only calculate based on X and Z distance, not Y (height)
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        float checkpointX = checkpoint.transform.position.x;
        float checkpointZ = checkpoint.transform.position.z;

        return Mathf.Sqrt(Mathf.Pow(playerX - checkpointX, 2f) + Mathf.Pow(playerZ - checkpointZ, 2f));
    }

    int GetPlayerPosition()
    {
        currentPosition = 1;

        players = GameObject.FindGameObjectsWithTag("Player");

        for (int n = 0; n < players.Length; n++)
        {
            int rivalLap = players[n].GetComponent<PlayerPositionMulti>().currentLap;
            if (currentLap < rivalLap)
                currentPosition++;
            else if (currentLap == rivalLap)
            {
                int rivalCheckpoint = players[n].GetComponent<PlayerPositionMulti>().currentCheckpoint;
                if (currentCheckpoint < rivalCheckpoint)
                    currentPosition++;
                else if (currentCheckpoint == rivalCheckpoint)
                {
                    float distanceToNext = DistanceCalc(this.gameObject, checkpoints.transform.GetChild((currentCheckpoint + 1) % numCheckpoints).gameObject);
                    float rivalDistance = DistanceCalc(players[n], checkpoints.transform.GetChild((rivalCheckpoint + 1) % numCheckpoints).gameObject);

                    if (distanceToNext > rivalDistance)
                        currentPosition++;
                }
            }
        }
        return currentPosition;
    }

    public void updateCheckpoint(int newCheckpoint)
    {
        if (newCheckpoint >= numCheckpoints - 1)
        {
            if (currentLap >= totalLaps)
            {
                currentLap++;
                if (!isLocalPlayer)
                    return;
                totalTime = Mathf.Floor(raceOverlay.GetComponent<StopWatch>().totalTime * 100);

                raceOverlay.GetComponent<StopWatch>().enabled = false;
                this.GetComponent<PlayerControllerMulti>().enabled = false;
                this.GetComponent<SlideMulti2>().enabled = false;
                this.GetComponent<HoverMotor>().enabled = false;

                if (totalTime < recordTime)
                {
                    CleanUp();
                    NewRecord();
                    //Invoke("CleanUp", 2.0f);
                    //Invoke("NewRecord", 2.0f);
                }
                else
                {
                    CleanUp();
                    ShowCredits();
                    //Invoke("CleanUp", 2.0f);
                    //Invoke("ShowCredits", 2.0f);
                }
            }
            else
            {
                Debug.Log("Player completed a lap");
                currentLap++;
                currentCheckpoint = -1;
            }
        }
        else
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
        results.text = placeText.text + "   " + timeText.text;
    }

    void NewRecord()
    {
        results.fontSize = 45;
        results.text = placeText.text + "   " + timeText.text + "   New Record!";
        if (mapNumber > 0)
        {
            lines[mapNumber - 1] = totalTime.ToString();
            File.WriteAllLines(Application.dataPath + "/recordTimes.txt", lines);
        }
    }

    public bool Finished()
    {
        if (currentLap > totalLaps)
        {
            return true;
        }

        return false;
    }
}

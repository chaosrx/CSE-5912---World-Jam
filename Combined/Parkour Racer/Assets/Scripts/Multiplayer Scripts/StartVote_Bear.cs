using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class StartVote_Bear : NetworkBehaviour {


    public string[] maps;

    //[SyncVar]
    public Text voteText;

    [SyncVar]
    public int countDownLenght;

    [SyncVar]
    private float totalTime;

    [SyncVar]
    private bool started;


    public GameObject[] MapButtons;

    void Start()
   {
        //maps[0] = GameObject.Find("Map 1");

        MapButtons[0].GetComponent<RectTransform>().localPosition = new Vector3(-575, 320, 0);
        MapButtons[1].GetComponent<RectTransform>().localPosition = new Vector3(-575, 240, 0);
        MapButtons[2].GetComponent<RectTransform>().localPosition = new Vector3(-575, 160, 0);
        MapButtons[3].GetComponent<RectTransform>().localPosition = new Vector3(-575, 80, 0);

        MapButtons[4].GetComponent<RectTransform>().localPosition = new Vector3(-575, -120, 0);
        MapButtons[5].GetComponent<RectTransform>().localPosition = new Vector3(-575, -200, 0);
        MapButtons[6].GetComponent<RectTransform>().localPosition = new Vector3(-575, -280, 0);
        MapButtons[7].GetComponent<RectTransform>().localPosition = new Vector3(-575, -360, 0);

    }

	// Use this for initialization
	public void StartVoteing () {
        if (isServer)
            started = true;

        if(started)
        voteText.text = countDownLenght.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!started)
        {}
        else if (totalTime < countDownLenght)
        {
            if(isServer)
                totalTime += Time.deltaTime;

            voteText.text = "" + Mathf.CeilToInt(countDownLenght - totalTime);
        }
        else
        {
            //NetworkManager.singleton.autoCreatePlayer = false;
            //GameObject MultiPlayerPrefab = NetworkManager.singleton.spawnPrefabs.
            GameObject MultiPlayerPrefab = Resources.Load<GameObject>("Player Multi (Timebox 5 Edition) 2");
            ClientScene.RegisterPrefab(MultiPlayerPrefab);
            NetworkManager.singleton.playerPrefab = MultiPlayerPrefab;
            NetworkManager.singleton.playerSpawnMethod = PlayerSpawnMethod.RoundRobin;
            //NetworkManager.singleton.autoCreatePlayer = true;
            //GetComponent<ClickToLoad>().ClickAsync(maps[GetMapNumber()]);
            if (isServer)
            {
                SceneManager.LoadScene(maps[GetMapNumber()]);
                NetworkManager.singleton.ServerChangeScene(maps[GetMapNumber()]);
            }
            this.enabled = false;           
        }

        if(started && !MapButtons[0].GetComponent<Button>().enabled)
        {
            MapButtons[0].GetComponent<Button>().enabled = true;
            MapButtons[1].GetComponent<Button>().enabled = true;
            MapButtons[2].GetComponent<Button>().enabled = true;
            MapButtons[3].GetComponent<Button>().enabled = true;
            MapButtons[4].GetComponent<Button>().enabled = true;
            MapButtons[5].GetComponent<Button>().enabled = true;
            MapButtons[6].GetComponent<Button>().enabled = true;
            MapButtons[7].GetComponent<Button>().enabled = true;

            /**
            GameObject map = GameObject.Find("Map 1");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 2");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 3");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 4");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 5");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 6");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 7");
            map.GetComponent<Button>().enabled = true;

            map = GameObject.Find("Map 8");
            map.GetComponent<Button>().enabled = true;
            **/
        }
    }

    public int GetMapNumber()
    {
        int maxVote = 0;
        int maxPosition = Random.Range(0, maps.Length);

        int[] voteTally = GetComponent<VoteTally>().voteTally;
        for (int n = 0; n < voteTally.Length; n++)
        {
            if (maxVote < voteTally[n])
            {
                maxVote = voteTally[n];
                maxPosition = n;
            }
        }
        
        return maxPosition;
    }
}

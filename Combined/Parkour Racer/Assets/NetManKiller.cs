using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetManKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1.0f;

        if (GameObject.Find("NetworkManager") != null)
        {
            Debug.Log("Killing host/server");
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopServer();

            GameObject MultiPlayerPrefab = Resources.Load<GameObject>("Empty Player Prefab");
            ClientScene.RegisterPrefab(MultiPlayerPrefab);
            NetworkManager.singleton.playerPrefab = MultiPlayerPrefab;
            NetworkManager.singleton.playerSpawnMethod = PlayerSpawnMethod.Random;
            //Destroy(GameObject.Find("NetworkManager"));
        }
        else
            Debug.Log("Could not find NetworkManager");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HostLobby : MonoBehaviour {

    public GameObject NetworkManager;
    private static NetworkManager networkManager;

    //NetworkLobbyManager networkLobbyManager;

    // Use this for initialization
    void Start () {
        if (networkManager == null)
            networkManager = NetworkManager.GetComponent<NetworkManager>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Host()
    {
        //networkLobbyManager = new NetworkLobbyManager();
        networkManager.StartHost();
        //GetComponent<Canvas>().enabled = false;
    }
}

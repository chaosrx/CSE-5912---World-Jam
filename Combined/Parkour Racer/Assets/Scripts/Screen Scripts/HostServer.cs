using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HostServer : MonoBehaviour {

    public GameObject NetworkManager;
    private static NetworkManager networkManager;

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
        networkManager.StartServer();
        //GetComponent<Canvas>().enabled = false;
    }
}

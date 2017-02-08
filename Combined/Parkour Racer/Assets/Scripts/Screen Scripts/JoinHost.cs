using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JoinHost : MonoBehaviour {

    public GameObject NetworkManager;
    private static NetworkManager networkManager;

    public InputField InputFeild;

    // Use this for initialization
    void Start () {
        if(networkManager == null)
            networkManager = NetworkManager.GetComponent<NetworkManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Join()
    {
        //this.networkManager.networkAddress = GameObject.Find("IP Address").ToString();
        networkManager.StartClient();
        //GetComponent<Canvas>().enabled = false;
    }

    public void SetTargetIP()
    {
        networkManager.networkAddress = InputFeild.text;
    }
}


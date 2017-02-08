using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ReturnToLobby : NetworkBehaviour {

    [SyncVar]
    public float elapsedTime;

    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (!p.GetComponent<PlayerPositionMulti>().Finished())
                return;
        }

        //Debug.Log("All finished");

        if (isServer)
            elapsedTime += Time.deltaTime;

        if (elapsedTime > 5f)
        {
            GameObject MultiPlayerPrefab = Resources.Load<GameObject>("Empty Player Prefab");
            ClientScene.RegisterPrefab(MultiPlayerPrefab);
            NetworkManager.singleton.playerPrefab = MultiPlayerPrefab;
            NetworkManager.singleton.playerSpawnMethod = PlayerSpawnMethod.Random;

            if (isServer)
            {
                SceneManager.LoadScene("Race Lobby");
                NetworkManager.singleton.ServerChangeScene("Race Lobby");
            }
        }

        return;
    }
}

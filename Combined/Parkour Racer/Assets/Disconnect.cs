using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Disconnect : MonoBehaviour {

    public void DisconnectToMain()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}

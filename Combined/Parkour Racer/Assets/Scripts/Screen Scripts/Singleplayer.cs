using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Singleplayer : MonoBehaviour {

    public string singleplayer;

    public void SingleplayerOnClick()
    {
        SceneManager.LoadSceneAsync(singleplayer);
    }
}

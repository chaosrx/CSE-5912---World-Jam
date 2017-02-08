using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Multiplayer : MonoBehaviour
{

    public string multiplayer;

    public void MultiplayerOnClick()
    {
        SceneManager.LoadSceneAsync(multiplayer);
    }
}

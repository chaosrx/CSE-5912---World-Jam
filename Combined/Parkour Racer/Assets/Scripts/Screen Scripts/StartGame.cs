using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public string mainMenu;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadSceneAsync(mainMenu);
        }
    }
}

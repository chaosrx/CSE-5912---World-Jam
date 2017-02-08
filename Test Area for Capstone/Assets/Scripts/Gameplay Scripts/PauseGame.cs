using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{

    public bool paused;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause();

        if (paused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(!paused);
        paused = !paused;
    }
}

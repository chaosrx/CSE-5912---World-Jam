using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{

    public void ReloadOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

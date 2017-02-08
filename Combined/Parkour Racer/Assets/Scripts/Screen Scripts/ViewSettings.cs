using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ViewSettings : MonoBehaviour {

    public string settings;

    public void ViewSettingsOnClick()
    {
        SceneManager.LoadSceneAsync(settings);
    }
}

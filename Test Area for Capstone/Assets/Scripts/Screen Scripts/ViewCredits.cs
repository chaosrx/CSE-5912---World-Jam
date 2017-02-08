using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ViewCredits : MonoBehaviour {

    public string credits;

    public void ViewCreditsOnClick()
    {
        SceneManager.LoadSceneAsync(credits);
    }
}

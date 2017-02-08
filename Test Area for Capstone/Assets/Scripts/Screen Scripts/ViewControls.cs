using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ViewControls : MonoBehaviour {

    public string controls;

	public void ViewControlsOnClick()
    {
        SceneManager.LoadSceneAsync(controls);
    }
}

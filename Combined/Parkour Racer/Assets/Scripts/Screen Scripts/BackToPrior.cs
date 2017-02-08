using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToPrior : MonoBehaviour {

    public string previousScene;

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadSceneAsync(previousScene);
        }
	}
}

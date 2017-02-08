using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayOnline : MonoBehaviour
{

    public string OnlineMap;

    public void OnlineMapOnClick()
    {
        SceneManager.LoadSceneAsync(OnlineMap);
        Destroy(GameObject.FindGameObjectWithTag("Menu Music"));
    }

}

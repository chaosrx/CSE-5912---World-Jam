using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoad : MonoBehaviour {

    public GameObject loadingScreen;
    public Text loadingPercentage;

    private AsyncOperation async;
    private string[] tips =
    {
        "While airborn perform a ground stomp for more accurate platforming",
        "To perform a triangle jump players must jump into a wall at less than a 60 degree angle",
        "To perform a wall jump players must jump into a wall between the angles of 60 and 90 degrees",
        "Combo an air dash into a wall jump or triangle jump to gain momentum",
        "Players speed warp bars charge faster or slower based on their position relative to other players",
        "Two of the games creators wanted the game concept to include horses, fortunately all they got was a cup",
        "Anything else you want to add guys..."
    };

    public void ClickAsync(string sceneName)
    {
        loadingScreen.SetActive(true);
        Text tipText = GameObject.Find("Tip Text").GetComponent<Text>();
        tipText.text = tips[(int)Random.Range(0.0f, (float)tips.Length)];
        StartCoroutine(LoadLevelWithBar(sceneName));
    }

    IEnumerator LoadLevelWithBar(string sceneName)
    {
        Slider loadingBar = GameObject.Find("Loading Bar").GetComponent<Slider>();
        async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            loadingPercentage.text = "Loading " + async.progress*100 + "%";
            loadingBar.value = async.progress;
            yield return null;
        }
    }
}

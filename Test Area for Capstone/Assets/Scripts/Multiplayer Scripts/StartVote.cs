using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartVote : MonoBehaviour {

    public string[] maps;
    public Text voteText;

    public int countDownLenght;

    private float totalTime;

	// Use this for initialization
	void Start () {
        voteText.text = countDownLenght.ToString();
	}
	
	// Update is called once per frame
	void Update () {

        if (totalTime < countDownLenght)
        {
            totalTime += Time.deltaTime;

            voteText.text = "" + Mathf.CeilToInt(countDownLenght - totalTime);
        }
        else
        {
            GetComponent<ClickToLoad>().ClickAsync(maps[GetMapNumber()]);
            this.enabled = false;
        }
    }

    public int GetMapNumber()
    {
        int maxVote = 0;
        int maxPosition = Random.Range(0, maps.Length);

        int[] voteTally = GetComponent<VoteTally>().voteTally;
        for (int n = 0; n < voteTally.Length; n++)
        {
            if (maxVote < voteTally[n])
            {
                maxVote = voteTally[n];
                maxPosition = n;
            }
        }
        
        return maxPosition;
    }
}

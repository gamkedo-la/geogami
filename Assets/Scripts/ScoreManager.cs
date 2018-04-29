using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject noOfFlips, sNoOfFlips;
    public GameObject resetter;
    public int CurrentScore { get; set; } // Number of flips

    private GameObject levelInfo;
    private int bestScore = int.MaxValue;
    private string bestScoreKey;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string bestScoreStr = "_bestScore";
        bestScoreKey = sceneName + bestScoreStr;


        levelInfo = GameObject.Find("Level Info");
        if (!levelInfo.GetComponent<LevelInfo>().isScoreable)
        {
            // Disable all score related objects
            noOfFlips.GetComponent<Text>().enabled = false;
            sNoOfFlips.GetComponent<Text>().enabled = false;
            resetter.SetActive(false);
            return;
        }

        CurrentScore = 0;
        if (PlayerPrefs.HasKey(bestScoreKey)) // Load score
            bestScore = PlayerPrefs.GetInt(bestScoreKey);

        if (resetter != null)
            resetter.GetComponent<Button>().onClick.AddListener(ResetScore);
    }

    void Update()
    {
        if (!levelInfo.GetComponent<LevelInfo>().isScoreable)
            return;

        if (levelInfo.GetComponent<LevelInfo>().IsLevelComplete)
        {
            bool beatScore = CurrentScore < bestScore;

            if (beatScore) // Save score
            {
                PlayerPrefs.SetInt(bestScoreKey, CurrentScore);
                bestScore = CurrentScore;
            }
        }
        UpdateDisplayText();
    }

    void UpdateDisplayText()
    {
        // print("Current score: " + CurrentScore);
        const string numOfFlipsDescriptor = "Number of flips: ";
        noOfFlips.GetComponent<Text>().text = numOfFlipsDescriptor +
            CurrentScore;

        const string sNumOfFlipsDescriptor = "Shortest number of flips: ";
        sNoOfFlips.GetComponent<Text>().text = sNumOfFlipsDescriptor +
            (bestScore == int.MaxValue ? "" : bestScore.ToString());
    }

    void ResetScore()
    {
        PlayerPrefs.DeleteKey(bestScoreKey);
        bestScore = int.MaxValue;
    }
}

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
    private bool saved;

    void Start()
    {
        
        levelInfo = GameObject.Find("Level Info");
        var levelInfoScript = levelInfo.GetComponent<LevelInfo>();
        if (!levelInfo.GetComponent<LevelInfo>().IsScoreable)
        {
            // Disable all score related objects
            noOfFlips.GetComponent<Text>().enabled = false;
            sNoOfFlips.GetComponent<Text>().enabled = false;
            resetter.SetActive(false);
            return;
        }

        CurrentScore = 0;
        bestScore = levelInfoScript.loadScore();

        if (resetter != null)
            resetter.GetComponent<Button>().onClick.AddListener(ResetScore);
    }

    void LateUpdate()
    {
        if (!levelInfo.GetComponent<LevelInfo>().IsScoreable)
            return;

        if (levelInfo.GetComponent<LevelInfo>().IsLevelComplete)
        {
            bool beatScore = CurrentScore < bestScore;

            if (beatScore) // Save score
            {
                PlayerPrefs.SetInt(bestScoreKey, CurrentScore);
                bestScore = CurrentScore;
                saved = true;
            }
        }
        //stop the flickring by drawing the text once.
        if (saved)
        {
            UpdateDisplayText();   
        }
    }

    void UpdateDisplayText()
    {
        // print("Current score: " + CurrentScore);
        const string numOfFlipsDescriptor = "Flips: ";
        noOfFlips.GetComponent<Text>().text = numOfFlipsDescriptor +
            CurrentScore;

        const string sNumOfFlipsDescriptor = "Best: ";
        sNoOfFlips.GetComponent<Text>().text = sNumOfFlipsDescriptor +
            (bestScore == int.MaxValue ? "-" : bestScore.ToString());
        saved = false;
    }

    void ResetScore()
    {
        PlayerPrefs.DeleteKey(bestScoreKey);
        bestScore = int.MaxValue;
    }

//    void OnGUI()
//    {
//        // Update UI text according to screen size
//
//        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
//
//        float resizeFactor = Screen.height;
//        Vector2 resizeVector = new Vector2(resizeFactor, resizeFactor / 5);
//
//        //noOfFlips.GetComponent<RectTransform>().sizeDelta = resizeVector;
//        noOfFlips.GetComponent<Text>().fontSize = (int)resizeFactor / 10;
//
//        //sNoOfFlips.GetComponent<RectTransform>().sizeDelta = resizeVector;
//        sNoOfFlips.GetComponent<Text>().fontSize = (int)resizeFactor / 10;
//    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{

    public string levelName;
    public int levelTrack;
    private bool isScoreable = true;
    public int initialBestScore = int.MaxValue;

    public bool IsLevelComplete { get; set; }

    // Use this for initialization
    void Start()
    {
        //look in playerPref.
        initialBestScore = loadScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int loadScore()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var bestScoreStr = "_bestScore";
        var bestScoreKey = sceneName + bestScoreStr;
        
        return PlayerPrefs.HasKey(bestScoreKey) ? PlayerPrefs.GetInt(bestScoreKey) : int.MaxValue;
    }

    public bool IsScoreable
    {
        get { return isScoreable; }
    }
}

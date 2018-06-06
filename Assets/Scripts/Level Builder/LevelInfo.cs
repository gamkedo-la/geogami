using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{

    
    private string levelName;
    private int levelTrack;
    //note: I've only made these fields private to force unity to rebuild with the default value.
    //this allowed me to set all levels to scorable as true with out loading all levels.
    //rexposing to the inspector you will have to edit each scene levelinfo. (sad times)
    private bool isScoreable = true;
    public int initialBestScore = int.MaxValue;

    public bool IsLevelComplete { get; set; }

    [SerializeField] private int buildIndexOffset = 3;
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
        levelName = SceneManager.GetActiveScene().name;
        levelTrack = SceneManager.GetActiveScene().buildIndex - buildIndexOffset;
        var bestScoreStr = "_bestScore";
        var bestScoreKey = levelName + bestScoreStr;
        
        return PlayerPrefs.HasKey(bestScoreKey) ? PlayerPrefs.GetInt(bestScoreKey) : int.MaxValue;
    }

    public string LevelName
    {
        get { return levelName; }
        set { levelName = value; }
    }

    public int LevelTrack
    {
        get { return levelTrack; }
        set { levelTrack = value; }
    }

    public bool IsScoreable
    {
        get { return isScoreable; }
    }
    
    
}

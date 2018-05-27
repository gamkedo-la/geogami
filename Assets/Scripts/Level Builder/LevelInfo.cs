using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{

    public string levelName;
    public int levelTrack;
    public bool isScoreable = false;
    public int initialBestScore = int.MaxValue;

    public bool IsLevelComplete { get; set; }

    // Use this for initialization
    void Awake()
    {
        if (initialBestScore == 0) initialBestScore = int.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

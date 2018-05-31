using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventManager : MonoBehaviour
{


    public float activateFadeDuration = 1f;

    public string gameStatus;


    public GameObject shapeTracker;
    ShapeTracker shapeTrackerScript;

	void Start()
	{
        gameStatus = "StartScene";
        shapeTrackerScript = shapeTracker.GetComponent<ShapeTracker>();
	}

	// --------------------
	// Main Game Loops
	// --------------------

	//void Update()
	//{
 //       if(gameStatus == "FlipMode")
 //       {
            
 //       } 
 //       else if(gameStatus == "StartScene")
 //       {
            
 //       }
 //       else if (gameStatus == "EndScene")
 //       {

 //       }
	//}


    // -----------
    // UI Buttons / Music
    // -----------

    public void homeButtonClicked()
    {

        Debug.Log("homeButtonClicked");

        // Switch to menu music
        GameObject musicManager = GameObject.Find("Music Controller");

        if (musicManager)
        {
            MusicController musicManagerScript = musicManager.GetComponent<MusicController>();
            musicManagerScript.PlayMenuMusic();
        }

        SceneManager.LoadScene("Menu - Levels");
    }

    public void resetLevelButtonClicked()
    {

        Debug.Log("resetButtonClicked");

        // Reload level
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

	// -----------
	// Level start
	// -----------

    public void activateLevel()
    {
        gameStatus = "Activated";
        shapeTrackerScript.levelActivate(activateFadeDuration);


    }

	// -----------
	// Level completed
	// -----------

	public void startLevelComplete()
    {
        gameStatus = "EndScene";
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventManager : MonoBehaviour
{


    public float activateFadeDuration = 1f;
    public float levelEndTime = 6f;

    public string gameStatus;


    public GameObject shapeTracker;
    ShapeTracker shapeTrackerScript;

    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;

	void Start()
	{
        gameStatus = "StartScene";
        shapeTrackerScript = shapeTracker.GetComponent<ShapeTracker>();

        // SFX
        sfxControllerGO = GameObject.Find("SFX Controller");
        if(sfxControllerGO)
        {
            sfxControllerScript = sfxControllerGO.GetComponent<sfxController>();
        }

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


        // Play sound effect
        if (sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_UI_Back();
        }

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

        // Play sound effect
        if (sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_UI_Select();
        }

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

        shapeTrackerScript.startLevelComplete();
        if(sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_LevelComplete();
        }

        StartCoroutine("endLevelComplete");
    }

    IEnumerator endLevelComplete()
    {
        // Get total number of scenes in build
       
        yield return new WaitForSeconds(levelEndTime);
       

        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            homeButtonClicked();
        }
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventManager : MonoBehaviour
{


    public float activateFadeDuration = 1f;
    public float levelEndTime = 6f;

    public string gameStatus;

    // Level start index
    int levelStartIndex = 3; // Needs to be updated


    public GameObject shapeTracker;
    ShapeTracker shapeTrackerScript;

    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;
    [SerializeField] private float revealSpeed = 60f;
    [SerializeField] private GameObject endScreen;

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

	    if (endScreen == null)
	    {
	        findEndScreen();
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

        // Record level reached
        int newLevelReached = SceneManager.GetActiveScene().buildIndex + 1 - levelStartIndex;
        int oldLevelReached = PlayerPrefs.GetInt("levelReached", 1);
        if(newLevelReached > oldLevelReached)
        {
            PlayerPrefs.SetInt("levelReached", newLevelReached);
        }

        StartCoroutine("displayLevelScore");
        StartCoroutine("endLevelComplete");
    }

    private void findEndScreen()
    {
        //using resourcse find all incase it isn'e enabled.
        var gos = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var go in gos)
        {
            if (go.name == "End Screen")
            {
                endScreen = go;
                endScreen.SetActive(true);
                endScreen.GetComponent<CanvasGroup>().alpha = 0;
                break;
            }    
        }
    }

    IEnumerator displayLevelScore()
    {
       // Get total number of scenes in build
        endScreen.SetActive(true);
        var canvasGroup = endScreen.GetComponent<CanvasGroup>();
        //lerp reveal the canvasGroup;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, revealSpeed * 0.3333f);
            yield return null;    
        }
        
    }

    IEnumerator endLevelComplete()
    {
        // Get total number of scenes in build
       
        yield return new WaitForSeconds(levelEndTime);
       

        if(SceneManager.GetActiveScene().name == "I Heart Shapes")
        {
            // Last level in series, return to main menu
            homeButtonClicked();
        }
        else if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            homeButtonClicked();
        }
       
    }

}

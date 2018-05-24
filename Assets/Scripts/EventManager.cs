using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

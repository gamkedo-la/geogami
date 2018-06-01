using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenTracker : MonoBehaviour {

    private bool levelComplete = false;

    public Transform shapeTracker;
    private ShapeTracker shapeTrackerScript;

    public Transform eventManager;
    private EventManager eventManagerScript;

    public Transform paintTracker;
    private PaintTracker paintTrackerScript;

    public Transform mainCamera;
    private CameraMovement mainCameraMovementScript;


    void Awake()
    {
        // Retrieve all Shapes from Level
        GameObject[] levelTokens = GameObject.FindGameObjectsWithTag("Token");

        foreach (GameObject token in levelTokens)
        {
            // Change parent to game core
            token.transform.SetParent(transform);
        }
    }


    void Start()
    {
        shapeTrackerScript = shapeTracker.GetComponent<ShapeTracker>();
        paintTrackerScript = paintTracker.GetComponent<PaintTracker>();
        mainCameraMovementScript = mainCamera.GetComponent<CameraMovement>();
        eventManagerScript = eventManager.GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1 && !levelComplete)
        {
            levelComplete = true;
            GameObject.Find("Level Info").GetComponent<LevelInfo>().IsLevelComplete = true;
            startLevelComplete();
        }
    }


    void startLevelComplete()
    {
        // Display level won, move to next level.

        Debug.Log("Level Completed");

        // 
        eventManagerScript.startLevelComplete();
        //shapeTrackerScript.startLevelComplete(); // Depracated 2018-06-01
        paintTrackerScript.startLevelComplete();
        mainCameraMovementScript.startLevelComplete();
    }
}

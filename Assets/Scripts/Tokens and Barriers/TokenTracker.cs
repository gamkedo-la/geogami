using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenTracker : MonoBehaviour {

    private bool levelComplete = false;

    public Transform shapeTracker;
    private ShapeTracker shapeTrackerScript;

    public Transform paintTracker;
    private PaintTracker paintTrackerScript;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1 && !levelComplete)
        {
            levelComplete = true;
            startLevelComplete();
        }
    }


    void startLevelComplete()
    {
        // Display level won, move to next level.

        Debug.Log("Level Completed");

        // Disable Player mechanics
        shapeTrackerScript.startLevelComplete();
        paintTrackerScript.startLevelComplete();
    }
}

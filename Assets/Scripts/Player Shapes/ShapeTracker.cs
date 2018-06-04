using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShapeTracker : MonoBehaviour
{
    public GameObject paintTracker;
    public GameObject paintSurface;
    public GameObject barrierTracker;
    public GameObject tokenTracker;
    public GameObject tokenSphere;


    public GameObject currentSelectedShape;
    Shape currentSelectedShapeScript;

    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;


    bool playerSelectedThisFrame = false;
    bool flippingEnabled = false;


    // --------------------
    // Main Game Loops
    // --------------------

    //AWAKE
    // Get all children from GameObject.find() and reassign this as parent
    void Awake()
    {
        // Retrieve all Shapes from Level
        GameObject[] levelShapes = GameObject.FindGameObjectsWithTag("Shape");

        foreach (GameObject startShape in levelShapes)
        {
            // Get color info from current parent
            ColorShape colorShapeScript = startShape.GetComponent<ColorShape>();
            colorShapeScript.overrideColorAll();

            // Change parent to game core
            startShape.transform.SetParent(transform);

            // Initialize with tracker connections
            colorShapeScript.initializeAll(paintTracker, paintSurface, barrierTracker, tokenTracker, tokenSphere);

            // Set to invisible to start

        }





    }

    void Start()
    {
        // SFX
        sfxControllerGO = GameObject.Find("SFX Controller");
        if (sfxControllerGO)
        {
            sfxControllerScript = sfxControllerGO.GetComponent<sfxController>();
        }


        // Select first shape to flip (if a shape is pre-selected in editor)
        if (currentSelectedShape)
        {
            // This allows first clicks to move shape, without having to select 
            // Important for single-shape levels?
            selectNewShape(currentSelectedShape);
        }
        else
        {
            // If only one shape, auto-select
            GameObject[] levelShapes = GameObject.FindGameObjectsWithTag("Shape");

            if (levelShapes.Length > 0)
            {
                selectNewShape(levelShapes[0]);
            }
        }
    }

    void Update()
    {
        if (flippingEnabled)
        {
            checkForPlayerSelection();
        }
    }

    void LateUpdate()
    {
        if (playerSelectedThisFrame)
        {
            playerSelectedThisFrame = false;
        }
        else if (flippingEnabled && currentSelectedShapeScript)
        {
            currentSelectedShapeScript.flipCheck();
        }
    }
  



    // -----------
    // Flipping
    // -----------
    public void setFlippingEnabled(bool temp)
    {
        flippingEnabled = temp;
    }


    // --------------------
    // PlayerSelection
    // --------------------

    public void checkForPlayerSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, 90.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Transform objectHit = hit.transform;

                Debug.Log("objectHit: " + objectHit);

                if (objectHit.tag == "Player")
                {
                    // Hit PlayerMesh, grandparent GO should be "Shape"
                    selectNewShape(objectHit.parent.parent.gameObject); 
                    playerSelectedThisFrame = true;
                    Debug.Log("Player Selected");

                    break;
                }

            }

        }
    }

    public void selectNewShape(GameObject newShape)
    {
        // Call deselect on old shape
        if (currentSelectedShapeScript)
        {
            currentSelectedShapeScript.deselect();
        }



        //if (currentSelectedShape == newShape)
        //{
        //    currentSelectedShape = null;
        //    currentSelectedShapeScript = null;

        //    Debug.Log("Same shape selected, deselected shape");
        //}
        //else
        //{
            currentSelectedShape = newShape;
            currentSelectedShapeScript = currentSelectedShape.GetComponent<Shape>();

            // Call select on new shape
            currentSelectedShapeScript.select();

            Debug.Log("Selected new shape");
        //}


        // Play sound effect
        if (sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_SelectShape();
        }



    }


    // -----------
    // Level Activate
    // -----------

    public void levelActivate(float duration)
    {

        StartCoroutine( setFlippingEnabledTrueAfterTime(duration) );

        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Shape")
            {
                Shape childScript = child.gameObject.GetComponent<Shape>();
                childScript.levelActivate(duration);
            }
        }


    }

    IEnumerator setFlippingEnabledTrueAfterTime(float time)
    {
  
        yield return new WaitForSeconds(time);
        setFlippingEnabled(true);
      
    }

    // -----------
    // Level completed
    // -----------

    public void startLevelComplete()
    {
        setFlippingEnabled(false);

        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Shape")
            {
                Shape childScript = child.gameObject.GetComponent<Shape>();
                childScript.startLevelComplete();
            }
        }
    }

}

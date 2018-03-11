using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTracker : MonoBehaviour {

    public float zTranslationStepSize = 0.5f;


    // --------------------
    // Main Game Loops
    // --------------------

    void Awake()
    {
        // Retrieve all Shapes from Level
        GameObject[] levelSurfaces = GameObject.FindGameObjectsWithTag("PaintSurface");

        //Transform paintSurfaceContainer = transform.Find("PaintSurface");

        foreach (GameObject startSurface in levelSurfaces)
        {
            // Change parent to game core
            startSurface.transform.SetParent(transform);
        }
    }


    // --------------------
    // Methods
    // --------------------

    public void addPaintTrail(GameObject paintTrailToAdd)
    {
        paintTrailToAdd.transform.SetParent(transform);
        translateStep();
    }

    public void translateStep()
    {
        Vector3 temp = transform.position;
        temp.z += zTranslationStepSize;
        transform.position = temp;

    }

    // -----------
    // Level completed
    // -----------

    public void startLevelComplete()
    {
        // Brighten all PaintTrails to final material

        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "PaintTrail")
            {
                PaintMaterials childScript = child.gameObject.GetComponent<PaintMaterials>();
                childScript.startLevelComplete();
            }
        }

    }
}

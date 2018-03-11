using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlippable : MonoBehaviour {

    public GameObject paintTracker;
    public GameObject myPlayerMesh;
    public PaintTrail paintTrailScript;

	// override from parents
    public void overrideColorMaterials (Material shapeMaterial, Material paintMaterial, Material completedMaterial) 
    {

        // pass along override to mesh
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Player")
            {
                PaintMaterials childScript = child.gameObject.GetComponent<PaintMaterials>();
                childScript.overridePaintMaterials(shapeMaterial, paintMaterial, completedMaterial);
            }
        }
	}
	

    public void initialize(GameObject paintTrackerGO, GameObject paintSurface, GameObject barrierTracker, GameObject tokenTracker, GameObject tokenSphere)
    {
        paintTracker = paintTrackerGO;
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Player")
            {
                PaintTrail childScript = child.gameObject.GetComponent<PaintTrail>();
                childScript.initialize(paintTrackerGO, paintSurface, barrierTracker, tokenTracker, tokenSphere);

                paintTrailScript = childScript;
            }
        }

    }


    public void instantiatePaintSurface(GameObject container)
    {
        paintTrailScript.instantiatePaintSurfaceElement(container);
    }
	
}

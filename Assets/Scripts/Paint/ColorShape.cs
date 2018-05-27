using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShape : MonoBehaviour
{

    Material shapeMaterial;
    Material paintMaterial;
    Material completedMaterial;


    // OVERRIDE FROM PARENT
    public void overrideColorAll()
    {
        // Level initialization before parent switch
        // Get color override info from parent if available, and send to children

        PlayerColorOverride colorOverrideScript = transform.parent.gameObject.GetComponent<PlayerColorOverride>();
        if (colorOverrideScript.shapeMaterial)
        {
            shapeMaterial = colorOverrideScript.shapeMaterial;
        }
        if (colorOverrideScript.paintMaterial)
        {
            paintMaterial = colorOverrideScript.paintMaterial;
        }
        if (colorOverrideScript.completedMaterial)
        {
            completedMaterial = colorOverrideScript.completedMaterial;
        }



        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Flippable")
            {
                ColorFlippable childScript = child.gameObject.GetComponent<ColorFlippable>();
                childScript.overrideColorMaterials(shapeMaterial, paintMaterial, completedMaterial);
            }
        }
    }

    public void initializeAll(GameObject paintTracker, GameObject paintSurface, GameObject barrierTracker, GameObject tokenTracker, GameObject tokenSphere)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Flippable")
            {
                ColorFlippable childScript = child.gameObject.GetComponent<ColorFlippable>();
                childScript.initialize(paintTracker, paintSurface, barrierTracker, tokenTracker, tokenSphere);
            }
        }
    }



}

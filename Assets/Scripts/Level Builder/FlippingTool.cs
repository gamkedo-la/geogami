using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingTool : MonoBehaviour {

    public GameObject shapesContainer;
    public GameObject paintSurfaceContainer;
    public GameObject barrierContainer;
    public GameObject tokenContainer;

    public GameObject selectedShape;
    public Shape selectedShapeScript;



    // -----------
    // Editor
    // -----------

  
    public void selectNewShape(GameObject newShape)
    {
        selectedShape = newShape;
        selectedShapeScript = selectedShape.transform.GetChild(0).gameObject.GetComponent<Shape>();
        selectedShapeScript.selectMainFlippableShape();

    }

    public void selectFirstShapeInShapeContainer()
    {
        selectNewShape(shapesContainer.transform.GetChild(0).gameObject);

    }

    public void flipShapeInEditor(Vector3 mouseClickPos)
    {
        selectedShapeScript.flipInEditor(mouseClickPos, paintSurfaceContainer);
    }



    public void paintAllShapesToPaintSurfaceEditor()
    {

        // Retrieve all Shapes from Level
        GameObject[] allPlayerShapes = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("allPlayerShapes " + allPlayerShapes);
        Debug.Log("allPlayerShapes length " + allPlayerShapes.Length);

        foreach (GameObject child in allPlayerShapes)
        {
            Debug.Log("foreach");
            PaintTrail childPaintTrailScript = child.GetComponent<PaintTrail>();

            if (child.activeSelf && childPaintTrailScript)
            {
                Debug.Log("activeSelf");
                childPaintTrailScript.instantiatePaintSurfaceElement(paintSurfaceContainer);
            }

        }
    }


    public void clearAllPaintSurface()
    {
        PaintSurfaceContainer pscScript = paintSurfaceContainer.GetComponent<PaintSurfaceContainer>();
        pscScript.clearAllPaintSurfaces();
    }

    public void clearAllBarriers()
    {
        BarrierContainer bcScript = barrierContainer.GetComponent<BarrierContainer>();
        bcScript.clearAllBarriers();
    }

    public void clearAllTokens()
    {
        TokenContainer tcScript = tokenContainer.GetComponent<TokenContainer>();
        tcScript.clearAllTokens();
    }


}

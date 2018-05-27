using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrail : MonoBehaviour
{
    // Runtime
    PaintMaterials paintMatScript;


    // Initialize
    public GameObject paintTracker;
    public GameObject paintSurface;
    public GameObject barrierTracker;
    public GameObject tokenTracker;
    public GameObject tokenSphere;

    public PaintTracker paintTrackerScript;

    public float paintSurfaceDistance = 1;
    public float shadowFadeTime = 3; //seconds
    public float shadowZTowardCamera = 3;







    public void initialize(GameObject paintTrackerGO, GameObject paintSurfaceGO, GameObject barrierTrackerGO, GameObject tokenTrackerGO, GameObject tokenSphereGO)
    {

        paintTracker = paintTrackerGO;
        paintSurface = paintSurfaceGO;
        barrierTracker = barrierTrackerGO;
        tokenTracker = tokenTrackerGO;

        tokenSphere = tokenSphereGO;

        // Scripts
        paintTrackerScript = paintTracker.transform.GetComponent<PaintTracker>();
        paintMatScript = gameObject.GetComponent(typeof(PaintMaterials)) as PaintMaterials;

    }






    // ---------------
    // Runtime
    // ---------------



    public void instantiatePaintTrail()
    {

        // Instantiate a copy of this shape
        GameObject myCopy = Instantiate(gameObject, transform.position, transform.rotation);
        PaintTrail myCopyPaintTrailScript = myCopy.GetComponent<PaintTrail>();
        PaintMaterials myCopyPaintMaterialsScript = myCopy.GetComponent<PaintMaterials>();

        // Change properties to Paint
        myCopyPaintTrailScript.transformIntoPaintTrail();
        myCopyPaintMaterialsScript.setMaterial(paintMatScript.paintMaterial);
        myCopyPaintMaterialsScript.overridePaintMaterials(paintMatScript.shapeMaterial, paintMatScript.paintMaterial, paintMatScript.completedMaterial);

        paintTrackerScript.addPaintTrail(myCopy);
    }

    public void transformIntoPaintTrail()
    {

        changeTag("PaintTrail");

    }


 
    public void transformIntoShadow()
    {
        changeTag("Shadow");
        paintMatScript = gameObject.GetComponent(typeof(PaintMaterials)) as PaintMaterials; // Initialize for ghost, since it was never initialized

        paintMatScript.setMaterial(paintMatScript.shadowMaterial);
        paintTrackerScript.addPaintTrail(gameObject);
        paintMatScript.fadeToAlpha(0f, shadowFadeTime);

        // Change z position so shadow appears above all objects
        Vector3 newPos = transform.position;
        newPos.z = -shadowZTowardCamera;
        transform.position = newPos;
    }



    // ---------------
    // Editor Mode
    // ---------------


    public void instantiatePaintSurfaceElement(GameObject container)
    {

        Debug.Log("instantiatePaintSurfaceElement");
        // Instantiate a copy of this player shape gameObject
        GameObject myCopy = Instantiate(gameObject, transform.position, transform.rotation, container.transform);

        // Initiallize it as a PaintSurface object
        PaintTrail myCopyPaintTrailScript = myCopy.GetComponent<PaintTrail>();
        myCopyPaintTrailScript.transformIntoPaintSurface();

        // Change material
        PaintMaterials myCopyPaintMaterialsScript = myCopy.GetComponent<PaintMaterials>();
        myCopyPaintMaterialsScript.setMaterial(myCopyPaintMaterialsScript.paintSurfaceMaterial);

        // Create tokens from vertices
        Debug.Log("Create Tokens");

    }

    public void transformIntoPaintSurface()
    {
        //transformChildObjectsIntoTokensEditor();
        changeTag("PaintSurface");
        //disableAllScripts();

        // Set z distance to canvas distance
        Vector3 temp = transform.position;
        temp.z = paintSurfaceDistance;
        transform.position = temp;
    }


    // ---------------
    // Helper Functions
    // ---------------


    void changeTag(string newTag)
    {
        gameObject.tag = newTag;
    }


    // ---------------
    // Level Activate
    // ---------------

    public void instantiatePaintTrailAfterTimeMain(float time)
    {
        if (gameObject.tag == "Player")
        {
            StartCoroutine(InstantiatePaintTrailAfterTime(time));
        }
    }

    IEnumerator InstantiatePaintTrailAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // First paint trail at start location
        instantiatePaintTrail();
    }


    public void fadeToFullAlpha(float duration)
    {
        paintMatScript.fadeToAlphaNoDestroy(1, duration);
    }

    public void fadeToZeroAlpha(float duration)
    {
        paintMatScript.fadeToAlphaNoDestroy(0, duration);
    }



    // ---------------
    // Level Complete
    // ---------------



    // ---------------
    // Depracated
    // ---------------

    //void disableAllScripts()
    //{

    //    MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
    //    foreach (MonoBehaviour script in scripts)
    //    {
    //        if (script.GetType() != typeof(PaintTrail))
    //        {
    //            script.enabled = false;
    //        }
    //    }
    //}


    //// ---------------
    //// Editor Helper Functions
    //// ---------------

    //void transformChildObjectsIntoTokensEditor()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        // Create token at this position
    //        GameObject newToken = Instantiate(tokenSphere, child.position, child.rotation, tokenTracker.transform);

    //    }
    //}

    //void destroyChildObjectsEditor()
    //{
    //    List<Transform> toDestroy = new List<Transform>();

    //    foreach (Transform child in transform)
    //    {
    //        toDestroy.Add(child);
    //    }

    //    foreach (Transform child in toDestroy)
    //    {
    //        DestroyImmediate(child.gameObject);
    //    }

    //}

    //public void startLevelComplete()
    //{
    //    paintMatScript.lerpBetweenMaterials(GetComponent<Renderer>().material, paintMatScript.completedMaterial, paintMatScript.lerpDuration);

    //}

}

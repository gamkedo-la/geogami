using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineVert : MonoBehaviour {

    Transform vertToFollow;
    Transform nextOutlineVert;

    Transform myLineCube;
    Transform mySphere;

    float zPlane;

	void Awake()
	{
        myLineCube = transform.Find("Line");
        mySphere = transform.Find("Sphere");
	}

    public void setzPlane(float z)
    {
        zPlane = z;

        Vector3 pos = transform.position;
        pos.z = zPlane;
        transform.position = pos;
    }

	public void setVertToFollow(Transform vert, Transform next)
    {
        vertToFollow = vert;
        nextOutlineVert = next;
    }

    public void updatePosition()
    {
        Vector3 newPos = transform.position;
        newPos.x = vertToFollow.position.x;
        newPos.y = vertToFollow.position.y;
        newPos.z = zPlane;

        transform.position = newPos;
    }

    public void updateLine()
    {
        Calculation.cubeBetweenPoints(myLineCube, transform, nextOutlineVert);
    }


    // --------------
    // Mesh
    // --------------

    public void setLineThickness(float newLineThickness)
    {
        //Debug.Log("myLineCube: " + myLineCube);
        //Debug.Log("myLineCube.localScale: " + myLineCube.localScale);

        // Line
        Vector3 newScale = myLineCube.localScale;
        newScale.x = newLineThickness;
        newScale.y = newLineThickness;
        myLineCube.localScale = newScale;


    }

    public void setSphereSize(float newSphereSize)
    {
      
        // Sphere
        Vector3 newScaleSphere = mySphere.localScale;
        newScaleSphere.x = newSphereSize;
        newScaleSphere.y = newSphereSize;
        newScaleSphere.z = newSphereSize;
        myLineCube.localScale = newScaleSphere;
    }

    // -----------
    // Level completed
    // -----------

    public void startLevelComplete()
    {
        fadeOutlineToZeroAlpha();
    }


    // -----------
    // Fade / Set Alpha
    // -----------

    public void setOutlineToAlpha(float alphaFinal)
    {

        Color newColor = myLineCube.GetComponent<Renderer>().material.color;
        newColor.a = alphaFinal;

        myLineCube.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        mySphere.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

 

    public void fadeOutlineToZeroAlpha()
    {

        StartCoroutine(outlineFade(0f, .5f)); // TODO remove Magic number, currently same as SHAPE's fade magic number
    }

    public void fadeOutlineToFullAlpha()
    {

        StartCoroutine(outlineFade(1f, .5f)); // TODO remove Magic number, currently same as SHAPE's fade magic number
    }

    IEnumerator outlineFade(float alphaFinal, float aTime)
    {
        float alpha = myLineCube.GetComponent<Renderer>().material.color.a;
        Color newColor = myLineCube.GetComponent<Renderer>().material.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {

            newColor.a = Mathf.Lerp(alpha, alphaFinal, t);

            myLineCube.GetComponent<Renderer>().material.SetColor("_Color", newColor);
            mySphere.GetComponent<Renderer>().material.SetColor("_Color", newColor);

            yield return null;
        }

        newColor.a = alphaFinal;

        myLineCube.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        mySphere.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        //Destroy(gameObject);  // Removed 2018-04-17 by Erik, unnecessary to destroy outline
    }
}

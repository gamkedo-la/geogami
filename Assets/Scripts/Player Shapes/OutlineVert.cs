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
        Debug.Log("myLineCube: " + myLineCube);
        Debug.Log("myLineCube.localScale: " + myLineCube.localScale);

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
}

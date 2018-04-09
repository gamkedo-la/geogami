using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour {

    public float zPlaneForOutline =-5f;

    public float defaultLineThickness = 0.05f;
    public float defaultSphereSize = 0.07f;
    public float selectedLineThickness = 0.07f;
    public float selectedSphereSize = 0.09f;

    public float hoverLineThickness = 0.08f;
    public float hoverSphereSize = 0.1f;

    bool selected;


    public GameObject verticesGO;
    Vertices verticesScript;

    List<Transform> verticesList;
    int vertcount;
    List<Transform> outlineList;
    //List<OutlineVert> outlineScriptList;

    // --------------
	// Initialization
    // --------------
	void Start () {
        verticesScript = verticesGO.GetComponent<Vertices>();

        verticesList = verticesScript.getVertices();
        outlineList = getOutlineVerts();

        vertcount = verticesList.Count;

        // Establish connection to verts it will follow
        linkOutlineVerts();

        // Set everything to default
        deselect();

	}

    public List<Transform> getOutlineVerts()
    {
        List<Transform> outlineVertices = new List<Transform>();
        foreach (Transform child in transform)
        {        
            // Set z to zPlane for outline objects
            OutlineVert outlineVertScript = child.GetComponent<OutlineVert>();
            outlineVertScript.setzPlane(zPlaneForOutline);

            // Add outline vert transform and script to lists
            outlineVertices.Add(child);
            //Debug.Log("child.GetComponent<OutlineVert>() " + child.GetComponent<OutlineVert>());
            //outlineScriptList.Add(child.GetComponent<OutlineVert>());
        }
        return outlineVertices;
    }



	// --------------
	// Run-time methods
	// --------------


	void Update()
	{
        updateOutlineVerts();
	}


	public void linkOutlineVerts()
    {
        // Link outlineVerts to shape vertices
        var i = 0;
        foreach (Transform outlineVert in outlineList)
        {
            OutlineVert outlineVertScript = outlineVert.GetComponent<OutlineVert>();
            outlineVertScript.setVertToFollow(verticesList[i], outlineList[ (i+1) % vertcount]);

            i++;
        }
    }


	public void updateOutlineVerts()
	{
        foreach (Transform outlineVert in outlineList)
        {
            OutlineVert outlineVertScript = outlineVert.GetComponent<OutlineVert>();
            outlineVertScript.updatePosition();

        }

        foreach (Transform outlineVert in outlineList)
        {
            OutlineVert outlineVertScript = outlineVert.GetComponent<OutlineVert>();
            outlineVertScript.updateLine();

        }
        
	}

    // --------------
    // Mesh
    // --------------

    public void setLineAndSphere(float newLineThickness, float newSphereSize)
    {
        foreach (Transform outlineVert in outlineList)
        {
            OutlineVert outlineVertScript = outlineVert.GetComponent<OutlineVert>();

            // Line
            outlineVertScript.setLineThickness(newLineThickness);

            // Sphere
            outlineVertScript.setSphereSize(newSphereSize);
        }
    }

    // -----------
    // Selection
    // -----------

    public void deselect()
    {
        setLineAndSphere(defaultLineThickness, defaultSphereSize);
        selected = false;
    }

    public void select()
    {
        setLineAndSphere(selectedLineThickness, selectedSphereSize);
        selected = true;
    }

    public void hoverOver()
    {
        setLineAndSphere(hoverLineThickness, hoverSphereSize);
    }

    public void hoverExit()
    {
        if(selected)
        {
            setLineAndSphere(selectedLineThickness, selectedSphereSize);
        }
        else
        {
            setLineAndSphere(defaultLineThickness, defaultSphereSize);
        }

    }
}

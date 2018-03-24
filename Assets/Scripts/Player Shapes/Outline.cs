using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour {

    GameObject verticesGO;
    Vertices verticesScript;

    List<Transform> verticesList;
    List<Transform> outlineList;

	// Use this for initialization
	void Start () {
        verticesScript = verticesGO.GetComponent<Vertices>();

        verticesList = verticesScript.getVertices();
        outlineList = getOutlineVerts();

        var i = 0;
        foreach (Transform vert in verticesList)
        {
            Debug.Log("i " + i + "  : " + vert + " " + outlineList[i++]);
        }

	}

    public List<Transform> getOutlineVerts()
    {
        List<Transform> outlineVertices = new List<Transform>();
        foreach (Transform child in transform)
        {
            outlineVertices.Add(child);

        }
        return outlineVertices;
    }


	void Update () {
       
	}


	public void followVertices()
	{
        
	}


}

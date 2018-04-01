using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertices : MonoBehaviour {

    public List<Transform> getVertices()
    {
        List<Transform> vertices = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.tag == "Vertex")
            {
                vertices.Add(child);
            }
         
        }

        return vertices;
    }

}

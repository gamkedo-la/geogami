using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


public class TokenGridCreator : MonoBehaviour
{

    [Tooltip("Size of the rectangle, and how far in +z the raycast will check.")]
    public Vector3 gridSize;

    [Tooltip("Density of gridpoints in x and y in points/distance. When set to x=1 & y=1, one gridpoint per unit square.")]
    public Vector2 gridDensity;

    List<Vector3> gridPoints = new List<Vector3>();
    List<Vector3[]> gridEdgePairs = new List<Vector3[]>();

    public GameObject tokenToInstantiate;
    Token tokenSphereScript;
    public Transform tokenContainer;


    public void initialize()
    {
        tokenSphereScript = tokenToInstantiate.GetComponent<Token>();
        updateGridSize();
    }

    // -----------
    // Token creation
    // -----------

    public void instantiateTokenGrid()
    {
        Debug.Log("instantiateTokenGrid");

        foreach (Vector3 point in gridPoints)
        {
            if (isValidTokenPoint(transform.position + point))
            {
                Vector3 correctTokenPosition = transform.position + point;
                correctTokenPosition.z = 0;
                instantiateToken(correctTokenPosition);
            }
        }

    }

    public void instantiateToken(Vector3 location)
    {
        GameObject newToken = PrefabUtility.InstantiatePrefab(tokenToInstantiate as GameObject) as GameObject;
        newToken.transform.position = location;
        newToken.transform.parent = tokenContainer;

    }

    public bool isValidTokenPoint(Vector3 gridpoint)
    {

        RaycastHit[] hits;

        hits = Physics.RaycastAll(gridpoint, Vector3.forward, gridSize.z);



        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Transform objectHit = hit.transform;

            //Debug.Log("Raycast objectHit.tag:" + objectHit.tag);

            if (objectHit.tag == "Player" || objectHit.tag == "PaintSurface")
            {

                return true;
            }

        }

        return false;
    }

    // -----------
    // Token Clear
    // -----------

    public void clearAllTokens()
    {

        var children = new List<Transform>();
        foreach (Transform child in tokenContainer.transform)
        {
            children.Add(child);
        }
        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }

    }


    // -----------
    // Grid and gizmos
    // -----------

    public void updateGridSize()
    {
        Debug.Log("updateGridSize()");

        // Empty old lists
        gridPoints.Clear();
        gridEdgePairs.Clear();

        // Calculate scaling and offset for grid creation
        Vector2 numberOfPoints = new Vector2(gridSize.x * gridDensity.x, gridSize.y * gridDensity.y);
        Vector2 scale = new Vector2(gridSize.x / numberOfPoints.x, gridSize.y / numberOfPoints.y);
        Vector3 offset = new Vector3(-gridSize.x / 2, -gridSize.y / 2, 0);


        // Create list for Gizmo gridlines
        for (int i = 0; i <= numberOfPoints.x; i++) // loop through gridSize.x + 1 times
        {
            for (int j = 0; j <= numberOfPoints.y; j++) // loop through gridSize.y + 1 times
            {
                //gridDensity / 2;
                Vector3 tempV3 = new Vector3(i * scale.x, j * scale.y, 0);
                tempV3 = tempV3 + offset;
                gridPoints.Add(tempV3);


                if (i == 0)
                {
                    // Add horizontal line edge pairs
                    Vector3[] tempV3_2 = new Vector3[2];
                    tempV3_2[0] = new Vector3(0, j * scale.y, 0);
                    tempV3_2[0] = tempV3_2[0] + offset;
                    tempV3_2[1] = new Vector3(gridSize.x, j * scale.y, 0);
                    tempV3_2[1] = tempV3_2[1] + offset;
                    gridEdgePairs.Add(tempV3_2);
                }

            }

            // Add vertical line edge pairs
            Vector3[] tempV3_1 = new Vector3[2];
            tempV3_1[0] = new Vector3(i * scale.x, 0, 0);
            tempV3_1[0] = tempV3_1[0] + offset;
            tempV3_1[1] = new Vector3(i * scale.x, gridSize.y, 0);
            tempV3_1[1] = tempV3_1[1] + offset;
            gridEdgePairs.Add(tempV3_1);



        }



    }




    void OnDrawGizmosSelected()
    {

        Gizmos.color = new Color(.5f, 1f, .5f, 0.1f);
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z + gridSize.z / 2), gridSize);

        // Draw red borders
        Gizmos.color = new Color(0, 1, 0, 0.9f);
        for (float i = -1; i < 2; i = i + 2)
        {

            Vector3 tempV3start = transform.position;
            Vector3 tempV3end = transform.position;
            tempV3start.x = tempV3start.x + i * (gridSize.x / 2 + 0.5f);
            tempV3start.y = tempV3start.y + (gridSize.y / 2 + 0.5f);
            tempV3end.x = tempV3end.x + i * (gridSize.x / 2 + 0.5f);
            tempV3end.y = tempV3end.y - (gridSize.y / 2 + 0.5f);

            Gizmos.DrawLine(tempV3start, tempV3end);

            tempV3start = transform.position;
            tempV3end = transform.position;
            tempV3start.x = tempV3start.x + (gridSize.x / 2 + 0.5f);
            tempV3start.y = tempV3start.y + i * (gridSize.y / 2 + 0.5f);
            tempV3end.x = tempV3end.x - (gridSize.x / 2 + 0.5f);
            tempV3end.y = tempV3end.y + i * (gridSize.y / 2 + 0.5f);

            Gizmos.DrawLine(tempV3start, tempV3end);

        }




        // Draw gray grid
        Gizmos.color = new Color(.3f, .7f, .3f, .8f);
        foreach (Vector3[] item in gridEdgePairs)
        {

            Gizmos.DrawLine(transform.position + item[0], transform.position + item[1]);

        }

    }

}

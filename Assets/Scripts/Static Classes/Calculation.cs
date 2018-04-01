using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculation
{


    public static void pointsToCapsule(GameObject capsule, Vector3 p1, Vector3 p2) 
    {
        // Make capsule game object have start and end points at p1 and p2.
        Debug.Log("pointsToCapsule");
        Debug.Log("capsule " + capsule);
        Debug.Log("p1 " + p1);
        Debug.Log("p2 " + p2);
    }

    public static void capsuleColToPoints(Transform capsule, Transform start, Transform end)
    {
        // Make capsule game object have start and end points at p1 and p2.
        Debug.Log("pointsToCapsule");
        Debug.Log("capsule " + capsule);
        Debug.Log("p1 " + start);
        Debug.Log("p2 " + end);

        CapsuleCollider capsulecol = capsule.GetComponent<CapsuleCollider>();
        Vector3 p1 = start.position;
        Vector3 p2 = end.position;

        // Reset capsule rotation to x axis

        // Calculate midpoint of p1 and p2
        // set capsule transform to midpoint
        // Calculate length of line between p1 and p2
        // Set capsule length to that length




    //Make sure that your capsule's long axis is along the x axis when its rotation transform is zero. 
       // (ie. when the capsules rotation = Quaternion.identity, q1 = q0 + Vector3.right * capsuleLength).

    //Rotate the capsule by Quaternion.FromToRotation(Vector3.right, (A - B).normalize)

    //Set the position to be(A + B) / 2

    //Set the scale of the capsule to(A - B).magnitude

    }

    public static void cubeBetweenPoints(Transform cube, Transform startTrans, Transform endTrans)
    {

        Vector3 startPos = startTrans.position;
        Vector3 endPos = endTrans.position;
        Vector3 centerPos = new Vector3(startPos.x + endPos.x, startPos.y + endPos.y, startPos.z + endPos.z) / 2f;
        Vector3 direction = endTrans.position - startTrans.position;
        //Vector3 centerPos = (endTrans.position - startTrans.position)/2f;


        // Reset cube
        cube.rotation = Quaternion.identity;

        // Set cube position to midpoint of start and end point
        cube.transform.position = centerPos;

        // Rotate to look at endpoint
        cube.rotation = Quaternion.LookRotation(direction);

        // set z scale to length between p1 and p2
        Vector3 newScale = cube.localScale;
        newScale.z = direction.magnitude;
        cube.localScale = newScale;


    }
}

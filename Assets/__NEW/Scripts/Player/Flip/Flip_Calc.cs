using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip_Calc : MonoBehaviour
{

    Vector3 getSwipeDirectionUsingClickPos(Vector3 mouseClickPos) // In game or edit mode
    {
        mouseClickPos.z = 0f;

        // Find V3 direction from Game Object to mouse
        Vector3 directionToMouse = mouseClickPos - transform.position;

        return directionToMouse;
    }


    public float angleBetweenVerts(Vector3 vert1, Vector3 vert2, Vector3 axis) // In game or edit mode
    {
        vert1.z = 0f;
        vert2.z = 0f;

        // Find V3 direction from Game Object to verts
        Vector3 directionToVert1 = vert1 - transform.position;
        Vector3 directionToVert2 = vert2 - transform.position;
        Vector3 directionToAxis = axis - transform.position;

        // Find angle in degrees between vert1 to vert2
        float vert2Angle = Vector3.SignedAngle(directionToVert1, directionToVert2, directionToAxis);

        return signedAngleTo360(vert2Angle);
    }

    public float signedAngleTo360(float angle)
    {
        if(angle < 0)
        {
            angle = 360f + angle;
        }
        else if (angle > 179.980f && angle < 179.981f) // Get rid of weird signedAngle issue
        {
            angle = 180;
        }

        return angle;
    }



    //// ####################
    //// Rotate system around center Transform of the parent object so that swipeDirection lies on the x-axis. 
    //// Then, find two max x values, and record both of those transforms in max_two_transforms.
    //// ####################



    //// --------------------------------------
    //// Determine rotation degrees from swipeDir to x-axis
    //float angleToXaxis = Mathf.Acos(Vector3.Dot(swipeDirection.normalized, Vector3.right)) * 180 / Mathf.PI;
    //float anglesign = Mathf.Sign(Vector3.Cross(swipeDirection, Vector3.right).z);
    //angleToXaxis = angleToXaxis* anglesign;

    //// --------------------------------------
    //// Get ordered list of all V3s in relation to transform.position for parent shape
    //for (int i = 0; i<vertices_transforms.Length; i++)
    //{
    //    vert_V3s[i] = vertices_transforms[i].position - transform.position;
    //              //Debug.Log ("vert_V3s[" + i + "]: " + vert_V3s [i]);
    //}


    //// --------------------------------------
    //// Rotate all V3s by -swipeDirection to make everything line up with x axis.
    //for (int i = 0; i<vert_V3s.Length; i++)
    //{
    //    vert_V3s[i] = Quaternion.Euler(0, 0, angleToXaxis) * vert_V3s[i];
    //              //Debug.Log ("Quaternion.Euler(0, 0, angleToXaxis): " + Quaternion.Euler (0, 0, angleToXaxis));
    //              //Debug.Log ("vert_V3s[" + i + "]: " + vert_V3s [i]);
    //}
}
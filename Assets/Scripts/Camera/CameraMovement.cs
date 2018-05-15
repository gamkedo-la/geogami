using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public Vector3 levelStartPosition = new Vector3(-10f, 0f, -10f);
    public Vector3 levelPosition = new Vector3(0f, 0f, -10f);
    public Vector3 levelEndPosition = new Vector3(10f, 0f, -10f);

    public float smoothTime = 0.3F;
    public bool moving = false;


    Transform target;
    Vector3 velocity = Vector3.zero;



    // Use this for initialization
    void Start()
    {

    }



    IEnumerator cameraMoveFromToDuration(Vector3 pos1, Vector3 pos2, float duration)
    {
        yield return 0;
    }


//    StartCoroutine(LerpFromTo(transform.position, newDesiredPosition, 1f) );
//}
//}
 
//IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
//{
//    for (float t = 0f; t < duration; t += Time.deltaTime)
//    {
//        transform.position = Vector3.Lerp(pos1, pos2, t / duration);
//        yield return 0;
//    }
//    transform.position = pos2;
//}

    //public class ExampleClass : MonoBehaviour
    //{
    //    public Transform target;
    //    public float smoothTime = 0.3F;
    //    private Vector3 velocity = Vector3.zero;
    //    void Update()
    //    {
    //        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
    //        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    //    }
    //}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float delayMoveIn = 0f;
    public float durationMoveIn = 1f;
    public float delayMoveOut = 2f;
    public float durationMoveOut = 1f;

    public Vector3 cameraStartPosition = new Vector3(-30f, 0f, -10f);
    public Vector3 cameraLevelPosition = new Vector3(0f, 0f, -10f);
    public Vector3 cameraEndPosition = new Vector3(30f, 0f, -10f);

    float smoothTime = 0.3F;
    public bool moving = false;

    Vector3 startV3;
    Vector3 targetV3;
    Vector3 velocity = Vector3.zero;



    // Use this for initialization
    void Start()
    {
        
        cameraMoveFromTo(cameraStartPosition, cameraLevelPosition, delayMoveIn, durationMoveIn);

    }


    public void startLevelComplete()
    {
        cameraMoveFromTo(cameraLevelPosition, cameraEndPosition, delayMoveOut, durationMoveOut);
    }


    void cameraMoveFromTo(Vector3 pos1, Vector3 pos2, float delay, float duration)
    {
        
        transform.position = pos1;

        startV3 = pos1;
        targetV3 = pos2;

        float[] delay_duration_V2 = new float[2];
        delay_duration_V2[0] = delay;
        delay_duration_V2[1] = duration;

        StartCoroutine("cameraMove", delay_duration_V2);
    }


    IEnumerator cameraMove(float[] delay_duration_V2)
    {
        float delay = delay_duration_V2[0];
        float duration = delay_duration_V2[1];

        moving = true;

        yield return new WaitForSeconds(delay);

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startV3, targetV3, t / duration);
            yield return 0;
        }

        transform.position = targetV3;
        moving = false;
    }




}


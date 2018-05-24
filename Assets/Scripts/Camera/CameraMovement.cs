using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float delayMoveIn = 0f;
    public float durationMoveIn = 2f;
    public float delayMoveOut = 3f;
    public float durationMoveOut = 2f;

    public Vector3 cameraStartPosition = new Vector3(-30f, 0f, -10f);
    public Vector3 cameraLevelPosition = new Vector3(0f, 0f, -10f);
    public Vector3 cameraEndPosition = new Vector3(30f, 0f, -10f);

    float smoothTime = 0.3F;
    public bool moving = false;

    Vector3 startV3;
    Vector3 targetV3;
    Vector3 velocity = Vector3.zero;

    public GameObject eventManager;
    EventManager eventManagerScript;

    // Use this for initialization
    void Start()
    {
        
        cameraMoveFromTo(cameraStartPosition, cameraLevelPosition, delayMoveIn, durationMoveIn);


        eventManagerScript = eventManager.GetComponent<EventManager>();
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

        StartCoroutine("cameraMoveSin2", delay_duration_V2);
    }

    // Move camera, lerping between two positions using sin^2 from 0 to pi as a multiplier
    IEnumerator cameraMoveSin2(float[] delay_duration_V2)
    {
        float delay = delay_duration_V2[0];
        float duration = delay_duration_V2[1];

        moving = true;

        yield return new WaitForSeconds(delay);

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {

            // Find fraction along path according to sin^2(T), with T = pi/2 * percentage of time
            float lerp_fraction =  Mathf.Pow (Mathf.Sin( (Mathf.PI/2) * t / duration) , 2); //OLD: t / duration;

            transform.position = Vector3.Lerp(startV3, targetV3, lerp_fraction);
            yield return 0;
        }

        transform.position = targetV3;
        moving = false;

        eventManagerScript.activateLevel();
    }




}


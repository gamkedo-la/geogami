using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip_Manager : MonoBehaviour {

    public Flip_180 flip_180;
    public Flip_Input flip_Input;
    public Flip_Animation flip_Animation;
    public VertTracker vertTracker;


	void Update () {
        if (Input.anyKeyDown)
        {
            flip_Input.keyPressed();
        } 
        else if(Input.GetMouseButtonDown(0))
        {
            flip_Input.mouseClicked();
        }
	}

    public void attemptToFlip(Vector3 swipeDirection)
    {
        Debug.Log("attemptToFlip: " + swipeDirection);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip_Input : MonoBehaviour {

	
    public void keyPressed()
    {
        Vector3 swipeDirection = Vector3.zero;
        bool swipeKeyPressed = false;

        // Get direction
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            swipeDirection = new Vector3(0.01f, 1f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            swipeDirection = new Vector3(1f, -0.01f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            swipeDirection = new Vector3(-1f, 0.01f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.X))
        {
            swipeDirection = new Vector3(-0.01f, -1f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            swipeDirection = new Vector3(-1f, 1f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            swipeDirection = new Vector3(1f, 1f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            swipeDirection = new Vector3(1f, -1f, 0f);
            swipeKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            swipeDirection = new Vector3(-1f, -1f, 0f);
            swipeKeyPressed = true;
        }


        if (swipeKeyPressed)
        {
            Debug.Log("Swipe: " + swipeDirection);

            GetComponent<Flip_Manager>().attemptToFlip(swipeDirection);
        }


    }

    public void mouseClicked()
    {
        // TODO - Mouse Click when shape is selected
        Debug.Log("mouseClicked: TODO");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{


    public GameObject flippable;
    public Flippable flippableScript; // Script of main child object, the primary fippable shape
    public ColorFlippable colorFlippableScript; // Script of main flippable shape to make paint trail
    Vector3 previousMousePos;

    // GHOST PLAYER
    bool ghosting = false; // True during one frame where ghost has been sent out in direction of click/swipe in order to verify valid path.
    int ghostTimer = 0;
    int ghostingWaitTime = 2; // Number of frames to wait

    // Use this for initialization
    void Start()
    {
        selectMainFlippableShape();

    }

    public void selectMainFlippableShape()
    {
        flippable = transform.GetChild(0).gameObject;
        flippableScript = transform.GetChild(0).GetComponent<Flippable>();
        flippableScript.initializeFlippableShape();

        colorFlippableScript = flippable.GetComponent<ColorFlippable>();
    }

    // Called once per frame when this shape is selected
    public void flipCheck()
    {

        if (!flippableScript.rotating)
        {
            if (ghosting)
            { // ghosting for specific number of frames to check if valid flip

                if (ghostTimer >= ghostingWaitTime)
                {
                    if (areThereGhosts())
                    {
                        // VALID TO FLIP PLAYER

                        // Move to new location
                        flippableScript.flip180DegAnimated(previousMousePos);

                        // Update number of flips
                        if (GameObject.Find("Score") != null)
                            GameObject.Find("Score").GetComponent<ScoreManager>().CurrentScore++;
                    }
                    else
                    {
                        // Cannot flip, display illegal flip animation
                        // Future TODO
                        Debug.Log("CANNOT FLIP");
                    }

                    // Reset
                    ghosting = false;
                    destroyGhosts();
                    ghostTimer = 0;
                }
                else
                {
                    ghostTimer++;
                }

            }
            else if (Input.GetMouseButtonDown(0))
            {
                // Get mouse position
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Send ghost of player ahead to check if flip is valid
                createAndFlipGhost(mousePos);
                ghosting = true;
                ghostTimer = 0;

                // Save click and wait an update frame before flipping shape
                previousMousePos = mousePos;
            }
        }


    }

    public void flipInEditor(Vector3 mouseClickPos, GameObject container)
    {
        flippableScript.flip180DegImmediately(mouseClickPos);
        colorFlippableScript.instantiatePaintSurface(container);
    }

    public void createAndFlipGhost(Vector3 mouseClickPos)
    {
        // Create ghost of player in same location, with this as parent
        GameObject ghost = Instantiate(flippable, transform);
        // Initialize
        Flippable ghostFlippableScript = ghost.GetComponent<Flippable>();
        ghostFlippableScript.initializeFlippableShape();
        // Make FlippableShape into Ghost
        ghostFlippableScript.makeIntoGhost();
        // Disable ghost mesh
        ghostFlippableScript.disableMeshRenderer();




        // Flip ghost towards click to check for collisions. Should be destroyed if it hits barrier object.
        ghostFlippableScript.flip180DegGhost(mouseClickPos);

    }





    bool areThereGhosts()
    {

        foreach (Transform child in transform)
        {
            Transform childPlayerMesh = child.Find("PlayerMesh");
            if (childPlayerMesh.tag == "PlayerGhost")
            {
                return true;
            }
        }
        return false;

    }

    void destroyGhosts()
    {

        foreach (Transform child in transform)
        {
            Transform childPlayerMesh = child.Find("PlayerMesh");

            if (childPlayerMesh.tag == "PlayerGhost")
            {
                Destroy(child.gameObject);
            }
        }

    }


    // -----------
    // Selection
    // -----------

    public void deselect()
    {
        flippableScript.deselect();
    }

    public void select()
    {
        flippableScript.select();
    }

    // -----------
    // Level completed
    // -----------

    public void startLevelComplete()
    {
        flippableScript.startLevelComplete();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippable : MonoBehaviour {

    // SWIPE / DIRECTION OF MOVEMENT
    private Vector3 target;
    private Transform[] max_two_transforms;
    private Transform other_transform;
    private Vector3[] vert_V3s;

    // ROTATION
    public GameObject vertices_GO;
    private Transform[] vertices_transforms;
    private Transform[] rotation_transforms;
    private Vector3 rotationPoint;
    private Vector3 rotationLine;
    private float rotationSign; // +/- 1
    private float rotationTracker;
    public float rotationSpeed = 30; // Degrees per frame, must be multiple of 180
    [HideInInspector]
    public bool rotating = false;

    // Painting
    public ColorFlippable colorFlippableScript;
    public PaintTrail paintTrailScript;

    // ----------------------------
    // Initialization
    // ----------------------------

    void Start()
    {
        initializeFlippableShape();

        paintTrailScript = transform.Find("PlayerMesh").gameObject.GetComponent(typeof(PaintTrail)) as PaintTrail;
        colorFlippableScript = gameObject.GetComponent<ColorFlippable>();
    }

    public void initializeFlippableShape()
    {

        // Initialize global variables used in functions
        target = transform.position;
        max_two_transforms = new Transform[2]; // Two max flipping corners
        rotation_transforms = new Transform[3]; // Three corners per rotation
                                                // Get child transforms in order
        vertices_transforms = new Transform[vertices_GO.transform.childCount];
        vert_V3s = new Vector3[vertices_transforms.Length]; // To store corner Vector3s
        for (int i = 0; i < vertices_transforms.Length; i++)
        {
            vertices_transforms[i] = vertices_GO.transform.GetChild(i).GetComponent<Transform>();
        }

        rotationTracker = 0;

    }


    // ----------------------------
    // Update every frame in-game
    // ----------------------------

    void Update()
    {
        if (rotating)
        {

            rotateStep();

        }
    }

    void rotateStep()
    {

        transform.parent.RotateAround(rotationPoint, rotationLine, rotationSign * rotationSpeed);
        rotationTracker += rotationSpeed;

        if (rotationTracker >= 180)
        {  // 180 degree flip
            rotationTracker = 0;
            rotating = false;
            setZToZero(); // Precaution against drift in z-axis

            // Create paint trail in new location
            paintTrailScript.instantiatePaintTrail(); //$$$$$$##############################################################
            //$$$$$$##############################################################
            //$$$$$$##############################################################
            //$$$$$$##############################################################
        }



    }

    // ----------------------------
    // Methods
    // ----------------------------

    public void flip180DegAnimated(Vector3 mouseClickPos)
    {
        Vector3 direction = getSwipeDirectionUsingClickPos(mouseClickPos);
        prepareToRotateTowards(direction);
        rotating = true;
    }

    public void flip180DegImmediately(Vector3 mouseClickPos)
    {
        Vector3 direction = getSwipeDirectionUsingClickPos(mouseClickPos);
        prepareToRotateTowards(direction);
        transform.parent.RotateAround(rotationPoint, rotationLine, rotationSign * 180);

        setZToZero(); // Precaution against drift in z-axis
    }

    public void flip180DegGhost(Vector3 mouseClickPos)
    {
        Vector3 direction = getSwipeDirectionUsingClickPos(mouseClickPos);
        prepareToRotateTowards(direction);
        transform.RotateAround(rotationPoint, rotationLine, rotationSign * 180);

        setZToZero(); // Precaution against drift in z-axis
    }

    void prepareToRotateTowards(Vector3 direction)
    {
        findTwoMaxCornersAlongDirection(direction);
        rotateAroundCornerPoints(max_two_transforms[0], max_two_transforms[1], other_transform);

    }

    void rotateAroundCornerPoints(Transform corner0, Transform corner1, Transform cornerInt)
    {

        rotation_transforms[0] = corner0;
        rotation_transforms[1] = corner1;
        rotation_transforms[2] = cornerInt; // Interior of shape

        rotationPoint = corner0.position;
        rotationLine = corner1.position - corner0.position;
        Vector3 tempV3Line = cornerInt.position - corner0.position;

        Vector3 tempV3CrossProduct = Vector3.Cross(rotationLine, tempV3Line);

        rotationSign = Mathf.Sign(-tempV3CrossProduct.z);
    }

    // TODO Clean up this messy code, move to different functions

    void findTwoMaxCornersAlongDirection(Vector3 swipeDirection)
    {
        // ####################
        // Rotate system around center Transform of the parent object so that swipeDirection lies on the x-axis. 
        // Then, find two max x values, and record both of those transforms in max_two_transforms.
        // ####################



        // --------------------------------------
        // Determine rotation degrees from swipeDir to x-axis
        float angleToXaxis = Mathf.Acos(Vector3.Dot(swipeDirection.normalized, Vector3.right)) * 180 / Mathf.PI;
        float anglesign = Mathf.Sign(Vector3.Cross(swipeDirection, Vector3.right).z);
        angleToXaxis = angleToXaxis * anglesign;

        // --------------------------------------
        // Get ordered list of all V3s in relation to transform.position for parent shape
        for (int i = 0; i < vertices_transforms.Length; i++)
        {
            vert_V3s[i] = vertices_transforms[i].position - transform.position;
            //          Debug.Log ("vert_V3s[" + i + "]: " + vert_V3s [i]);
        }


        // --------------------------------------
        // Rotate all V3s by -swipeDirection to make everything line up with x axis.
        for (int i = 0; i < vert_V3s.Length; i++)
        {
            vert_V3s[i] = Quaternion.Euler(0, 0, angleToXaxis) * vert_V3s[i];
            //          Debug.Log ("Quaternion.Euler(0, 0, angleToXaxis): " + Quaternion.Euler (0, 0, angleToXaxis));
            //          Debug.Log ("vert_V3s[" + i + "]: " + vert_V3s [i]);
        }

        // --------------------------------------
        // Record x values in new array
        float[] vert_V3s_x = new float[vert_V3s.Length];
        float[] vert_V3s_y = new float[vert_V3s.Length];
        for (int i = 0; i < vert_V3s.Length; i++)
        {
            vert_V3s_x[i] = vert_V3s[i].x;
            vert_V3s_y[i] = vert_V3s[i].y;
        }


        // --------------------------------------
        // Locate index of two maximum x values, and index of another point

        int index_max_x_1;
        int index_max_x_2;
        int index_other = -1;

        // Find first largest element
        float value_x = Mathf.Max(vert_V3s_x);
        index_max_x_1 = System.Array.IndexOf(vert_V3s_x, value_x);
        // Erase first largest element (set to -999999f)

        vert_V3s_x[index_max_x_1] = -999999f;

        // Use y-value of first point to eliminate points on side that is invalid (WATCH OUT! TRICKY!)
        float value_matching_y_1 = vert_V3s_y[index_max_x_1];
        for (int i = 0; i < vert_V3s_x.Length; i++)
        {
            if (value_matching_y_1 <= 0)
            {
                if (vert_V3s_y[i] < value_matching_y_1)
                {
                    vert_V3s_x[i] = -999999f;
                }
            }
            else
            {
                if (vert_V3s_y[i] > value_matching_y_1)
                {
                    vert_V3s_x[i] = -999999f;
                }
            }
        }
        // Find second largest element.
        value_x = Mathf.Max(vert_V3s_x);
        index_max_x_2 = System.Array.IndexOf(vert_V3s_x, value_x);
        // Find a third index for third corner point
        for (int i = 0; i < vert_V3s_x.Length; i++)
        {
            if (i != index_max_x_1 && i != index_max_x_2)
            {
                index_other = i;
                break;
            }
        }

        //      Debug.Log("index_max_x_1: " + index_max_x_1);
        //      Debug.Log("index_max_x_2: " + index_max_x_2);
        //      Debug.Log("index_other: " + index_other);
        //      for (int i = 0; i < vert_V3s_x.Length; i++) {
        //          Debug.Log("vert_V3s_x[" + i + "]: " +  vert_V3s_x[i]);
        //      }
        //
        //      for (int i = 0; i < vertices_transforms.Length; i++) {
        //          Debug.Log("vertices_transforms[" + i + "]: " +  vertices_transforms[i].position);
        //      }

        // --------------------------------------
        // Save transforms to global array / variable

        max_two_transforms[0] = vertices_transforms[index_max_x_1];
        max_two_transforms[1] = vertices_transforms[index_max_x_2];
        other_transform = vertices_transforms[index_other];


    }

    /* Deprecated due to ghosting
    Vector3 getSwipeDirectionFromClick() // In game only
    {
        // Find mouse position using 2D orthographic camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Find V3 direction from Game Object to mouse
        Vector3 directionToMouse = mousePos - transform.position;

        return directionToMouse;
    }
    */

    Vector3 getSwipeDirectionUsingClickPos(Vector3 mouseClickPos) // In game or edit mode
    {
        mouseClickPos.z = 0f;

        // Find V3 direction from Game Object to mouse
        Vector3 directionToMouse = mouseClickPos - transform.position;

        return directionToMouse;
    }

    public void disableMeshRenderer()
    {
        // Get child GO
        Transform playerMesh = transform.Find("PlayerMesh");
        MeshRenderer meshRenderer = playerMesh.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void enableMeshRenderer()
    {
        // Get child GO
        Transform playerMesh = transform.Find("PlayerMesh");
        MeshRenderer meshRenderer = playerMesh.GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;


    }


    void setZToZero()
    {
        // Set z to zero as a precaution against drift in z axis 
        Vector3 tempV3 = transform.position;
        tempV3.z = 0;
        transform.position = tempV3;

    }


    // -----------
    // Ghost Mode
    // -----------

    public void makeIntoGhost()
    {
        //transform.tag = null;
        transform.Find("PlayerMesh").tag = "PlayerGhost";

        // Destroy all unnecessary elements
        GameObject outline = transform.Find("Outline").gameObject;
        Destroy(outline);
        GameObject barriers = transform.Find("barriers").gameObject;
        Destroy(barriers);
    }

    public void barrierHitGhost()
    {
        

        enableMeshRenderer();
        createShadow();
        Destroy(gameObject);



    }

    public void createShadow()
    {
        paintTrailScript.transformIntoShadow(); // Relocates playerMesh to Paint Tracker
    }

    // -----------
    // Level completed
    // -----------

    public void startLevelComplete()
    {
        // TODO if needed
    }
}
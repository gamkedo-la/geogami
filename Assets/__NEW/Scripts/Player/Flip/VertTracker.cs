using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertTracker: MonoBehaviour {

    public Flip_Calc flip_Calc;

    public Transform[] vertices;
    public Transform localPlusZ;
    public float[] angleZones; // Zones for flipping in degrees, first vertex is 0 deg


    void Start()
    {
        angleZones = new float[vertices.Length];
        calculateAllAngleZones();

    }



    // Angle zones represented by ordered float array, e.g. [0, 90, 180, 270] for square
    void calculateAllAngleZones()
    {
        for (int i = 1; i < angleZones.Length; i++)
        {
            angleZones[i] = flip_Calc.angleBetweenVerts(vertices[0].position, vertices[i].position, localPlusZ.position);
        }
    }




}

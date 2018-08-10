using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip_Animation : MonoBehaviour {



    [Tooltip("Speed of flip, 0-15")]
    public int flipSpeedIndex = 10;
    public int flipAngleSpeed;

    public int[] validRotationAngles = new int[17]; //TODO

	// Use this for initialization
	void Start () {
        //validRotationAngles = [1, 2, 3, 4, 5, 6, 8, 9, 10, 12, 15, 18, 20, 24, 30, 40, 60];
        validRotationAngles[0] = 1;
        validRotationAngles[1] = 2;
        validRotationAngles[2] = 3;
        validRotationAngles[3] = 4;
        validRotationAngles[4] = 5;
        validRotationAngles[5] = 6;
        validRotationAngles[6] = 8;
        validRotationAngles[7] = 9;
        validRotationAngles[8] = 10;
        validRotationAngles[9] = 12;
        validRotationAngles[10] = 15;
        validRotationAngles[11] = 18;
        validRotationAngles[12] = 20;
        validRotationAngles[13] = 24;
        validRotationAngles[14] = 30;
        validRotationAngles[15] = 40;
        validRotationAngles[16] = 60;

        flipAngleSpeed = validRotationAngles[flipSpeedIndex];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

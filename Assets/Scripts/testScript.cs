using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {

    public Transform v1;
    public Transform v2;

	// Use this for initialization
	void Update () {
        moveCubeToLine();
  	}
	
	// Update is called once per frame
    void moveCubeToLine () {
        Calculation.cubeBetweenPoints(transform, v1, v2);
	}
}

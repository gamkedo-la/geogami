﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour {

	// Use this for initialization
	public void resetGame () {
        PlayerPrefs.DeleteAll();
	}
	
	
}
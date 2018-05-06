﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

    public float baseScale = 0.2f;
    public float hoverScale = 0.25f;
    public string sceneToLoad;

    //public Image myTriangle;


	void Start () {
        setScaleXY(baseScale);

        //myTriangle.alphaHitTestMinimumThreshold = 0.5f;
	}

	public void myOnMouseEnter()
	{
        setScaleXY(hoverScale);

        Debug.Log("myOnMouseEnter");
	}

    public void myOnMouseExit()
    {
        setScaleXY(baseScale);
        Debug.Log("myOnMouseExit");
    }


    public void myOnMouseClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


	public void setScaleXY(float newScale)
    {
        Vector3 scaleTemp = transform.localScale;
        scaleTemp.x = newScale;
        scaleTemp.y = newScale;
        transform.localScale = scaleTemp;
    }

}

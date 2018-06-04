using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

    public float baseScale = 0.2f;
    public float hoverScale = 0.25f;
    public string sceneToLoad;
    
    public Color32 hoverColor;
    public Color32 idleColor;
    private Image myTriangle;

    //SFX
    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;
    private int levelStartIndex = 3;

    void Start () {
        
        if (sceneToLoad.Length == 0)
        {
            sceneToLoad = findLevelName();
        }
        
        setScaleXY(baseScale);

        myTriangle = gameObject.transform.GetChild(0).GetComponent<Image>(); // get the triangle image so we can change its color
        //myTriangle.alphaHitTestMinimumThreshold = 0.5f;


        // SFX
        sfxControllerGO = GameObject.Find("SFX Controller");
        if (sfxControllerGO)
        {
            sfxControllerScript = sfxControllerGO.GetComponent<sfxController>();
        }
	}

	public void myOnMouseEnter()
	{
        setScaleXY(hoverScale);
        myTriangle.color = hoverColor;
        Debug.Log("myOnMouseEnter");
        Debug.Log(gameObject.name);
	}

    public void myOnMouseExit()
    {
        setScaleXY(baseScale);
        myTriangle.color = idleColor;
        Debug.Log("myOnMouseExit");
        Debug.Log(gameObject.name);
    }


    public void myOnMouseClick()
    {
        
        SceneManager.LoadScene(sceneToLoad);

        // Play sound effect
        if (sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_UI_Select();
        }
    }


	public void setScaleXY(float newScale)
    {
        Vector3 scaleTemp = transform.localScale;
        scaleTemp.x = newScale;
        scaleTemp.y = newScale;
        transform.localScale = scaleTemp;
    }
    
    /// <summary>
    /// Didn't want to type out all the string names of the levels so built this helper function
    /// though I could use things from the Scenemanager class however they only work with loaded scens and not scenes
    /// that sit in the buildsettings
    /// </summary>
    /// <returns></returns>
    public string findLevelName()
    {
        //step down to find child text.
        var menuText = GetComponentInChildren<Text>();
        var buildIndex = Convert.ToInt32(menuText.text);

       
        //verify scene object or return null
        return System.IO.Path.GetFileNameWithoutExtension( SceneUtility.GetScenePathByBuildIndex( buildIndex + levelStartIndex ) );
        
    }
}

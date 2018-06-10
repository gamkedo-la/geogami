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

    public bool active = true;
    
    public Color32 hoverColor;
    public Color32 idleColor;
    public Color32 lockedColor;
    private Image myTriangle;
    private Image myLock;
    private Text myNumberText;

    //SFX
    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;
    private int levelStartIndex = 3;
    [SerializeField] private bool debugOn;
    void Awake () {
        
        if (sceneToLoad.Length == 0)
        {
            sceneToLoad = findLevelName();
        }

        setScaleXY(baseScale);

        myTriangle = gameObject.transform.GetChild(0).GetComponent<Image>(); // get the triangle image so we can change its color
        myLock = gameObject.transform.GetChild(1).GetComponent<Image>(); // get the lock image 
        myNumberText = gameObject.transform.GetChild(2).GetComponent<Text>(); // get the lock image 
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
        if(active)
        {
            setScaleXY(hoverScale);
            myTriangle.color = hoverColor;
        }

	  

	}

    public void myOnMouseExit()
    {
        if (active)
        {
            setScaleXY(baseScale);
            myTriangle.color = idleColor;
        }

     

    }


    public void myOnMouseClick()
    {
        if (active)
        {
            SceneManager.LoadScene(sceneToLoad);

            // Play sound effect
            if (sfxControllerScript)
            {
                sfxControllerScript.Play_SFX_UI_Select();
            }
        }
    }


	public void setScaleXY(float newScale)
    {
        Vector3 scaleTemp = transform.localScale;
        scaleTemp.x = newScale;
        scaleTemp.y = newScale;
        transform.localScale = scaleTemp;
    }

    public void setInteractable(bool interactable)
    {
        if (interactable)
        {
            active = true;
            unlockLevel();
        } 
        else
        {
            active = false;
            lockLevel();

        }
    }

    public void lockLevel()
    {
        
        myTriangle.color = lockedColor;
        myLock.enabled = true;
        myNumberText.enabled = false;
    }

    public void unlockLevel()
    {
        
        myTriangle.color = idleColor;
        myLock.enabled = false;
        myNumberText.enabled = true;
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

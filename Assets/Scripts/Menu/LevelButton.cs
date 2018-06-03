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

    void Start () {
        setScaleXY(baseScale);

        myTriangle = this.gameObject.transform.GetChild(0).GetComponent<Image>(); // get the triangle image so we can change its color
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
	}

    public void myOnMouseExit()
    {
        setScaleXY(baseScale);
        myTriangle.color = idleColor;
        Debug.Log("myOnMouseExit");
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

}

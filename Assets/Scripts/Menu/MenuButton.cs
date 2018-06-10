using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    

    public float baseScale = 1f;
    public float hoverScale = 1.25f;

    public string sceneToLoad = "Menu - Main";

    //public Image myTriangle;

    //SFX
    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;
    [SerializeField] private bool debugOn;

    void Start()
    {
        setScaleXY(baseScale);

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

       
    }

    public void myOnMouseExit()
    {
        setScaleXY(baseScale);
       

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

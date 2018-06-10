using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonBonus : MonoBehaviour
{



    public string sceneToLoad;


   

    //SFX
    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;


    void Awake()
    {
        
        // SFX
        sfxControllerGO = GameObject.Find("SFX Controller");
        if (sfxControllerGO)
        {
            sfxControllerScript = sfxControllerGO.GetComponent<sfxController>();
        }
    }

    public void myButtonClicked()
    {
        
        SceneManager.LoadScene(sceneToLoad);

        // Play sound effect
        if (sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_UI_Select();
        }

    }

}

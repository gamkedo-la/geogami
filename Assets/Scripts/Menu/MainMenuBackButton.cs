using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBackButton : MonoBehaviour {


    //SFX
    public GameObject sfxControllerGO;
    sfxController sfxControllerScript;

    void Start()
    {
        // SFX
        sfxControllerGO = GameObject.Find("SFX Controller");
        if (sfxControllerGO)
        {
            sfxControllerScript = sfxControllerGO.GetComponent<sfxController>();
        }
    }

    public void backButtonClicked()
    {



        SceneManager.LoadScene("Menu - Main");

        // Play sound effect
        if (sfxControllerScript)
        {
            sfxControllerScript.Play_SFX_UI_Back();
        }
    }
}

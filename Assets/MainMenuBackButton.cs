using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBackButton : MonoBehaviour {

    public void backButtonClicked()
    {

        Debug.Log("backButtonClicked");

        SceneManager.LoadScene("Menu - Main");
    }
}

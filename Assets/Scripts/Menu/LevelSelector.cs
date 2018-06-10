using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {


    public LevelButton[] level_buttons;

	// Use this for initialization
	void Start () 
    {
        //PlayerPrefs.SetInt("levelReached", 5);
        int levelreached = PlayerPrefs.GetInt("levelReached",1);
		
        for (int i = 0; i < level_buttons.Length; i++)
        {

            if(i + 1 > levelreached)
            {
                
                level_buttons[i].setInteractable(false);

                // Show Lock sprite
            }
            else if (i == levelreached)
            {
                level_buttons[i].setInteractable(true);

                // Show current level
            }
            else
            {
                level_buttons[i].setInteractable(true);

                // Show completed level
            }

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuButtonSelectable : MonoBehaviour {

	//   LevelButton parentLevelButtonScript;
	//   float baseScale;
	//   float hoverScale;

	//void Start () {
	//       parentLevelButtonScript = transform.parent.GetComponent<LevelButton>();
	//       baseScale = parentLevelButtonScript.baseScale;
	//       hoverScale = parentLevelButtonScript.hoverScale;
	//}
	public Image menuButtonImage;

	void Start()
	{
		if (!menuButtonImage)
		{
			menuButtonImage = GetComponent<Image>();
		}
		menuButtonImage.alphaHitTestMinimumThreshold = 1;
	}


	public void test1()
    {
        Debug.Log("HELLO");

    }
   
    public void test2()
    {
        Debug.Log("GOODBYE");
    }

}

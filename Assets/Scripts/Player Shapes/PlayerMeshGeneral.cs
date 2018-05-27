using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshGeneral : MonoBehaviour {

    Flippable parentFlippableScript;

	void Start()
	{
        parentFlippableScript = transform.parent.GetComponent<Flippable>();
	}

	void OnMouseOver()
    {
        if(parentFlippableScript)
        {
            parentFlippableScript.hoverOver();
        }
    }

    void OnMouseExit()
    {
        
        if (parentFlippableScript)
        {
            parentFlippableScript.hoverExit();
        }
    }
}

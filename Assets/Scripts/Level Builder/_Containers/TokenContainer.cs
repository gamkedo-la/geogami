using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenContainer : MonoBehaviour {

    // -----------
    // Token Clear
    // -----------

    public void clearAllTokens()
    {

        var children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }

    }

}

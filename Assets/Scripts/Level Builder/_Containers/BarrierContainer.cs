using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierContainer : MonoBehaviour {

    // -----------
    // Barrier Clear
    // -----------

    public void clearAllBarriers()
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

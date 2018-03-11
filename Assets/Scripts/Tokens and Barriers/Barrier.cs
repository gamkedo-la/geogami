using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerGhost"))
        {
            GameObject ghostFlippableGO = other.transform.parent.gameObject;
            Flippable ghostFlippableScript = ghostFlippableGO.GetComponent<Flippable>();

            ghostFlippableScript.barrierHitGhost();

        }
    }


}

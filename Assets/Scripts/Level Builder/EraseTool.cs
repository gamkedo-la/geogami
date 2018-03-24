using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseTool : MonoBehaviour {

    public GameObject paintSurfaceContainer;
    public GameObject barrierContainer;
    public GameObject tokenContainer;


    public void clearAllPaintSurface()
    {
        PaintSurfaceContainer pscScript = paintSurfaceContainer.GetComponent<PaintSurfaceContainer>();
        pscScript.clearAllPaintSurfaces();
    }

    public void clearAllBarriers()
    {
        BarrierContainer bcScript = barrierContainer.GetComponent<BarrierContainer>();
        bcScript.clearAllBarriers();
    }

    public void clearAllTokens()
    {
        TokenContainer tcScript = tokenContainer.GetComponent<TokenContainer>();
        tcScript.clearAllTokens();
    }
}

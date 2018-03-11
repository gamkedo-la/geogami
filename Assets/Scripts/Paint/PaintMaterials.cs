using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMaterials : MonoBehaviour
{

    public Material shapeMaterialDefault;
    public Material paintMaterialDefault;
    public Material completedMaterialDefault;

    public Material shapeMaterial;
    public Material paintMaterial;
    public Material completedMaterial;
    public Material shadowMaterial;
    public Material paintSurfaceMaterial;

    Material shapeMaterialOverride;
    Material paintMaterialOverride;
    Material completedMaterialOverride;



    // LERP - linear interpretation between materials
    private bool lerpingBetweenMaterials;
    private Material lerpMaterialStart;
    private Material lerpMaterialEnd;
    public float lerpDuration = 10.0f;
    private float lerpTime = 0f;
    private float lerp = 0f;


    // ---------------
    // Initialize
    // ---------------

    // override from parents
    public void overridePaintMaterials(Material shapeMaterial, Material paintMaterial, Material completedMaterial)
    {

        shapeMaterialOverride = shapeMaterial;
        paintMaterialOverride = paintMaterial;
        completedMaterialOverride = completedMaterial;
    }

    // ---------------
    // Runtime
    // ---------------

    void Start()
    {
        
        shapeMaterial = shapeMaterialOverride ? shapeMaterialOverride : shapeMaterialDefault;
        paintMaterial = paintMaterialOverride ? paintMaterialOverride : paintMaterialDefault;
        completedMaterial = completedMaterialOverride ? completedMaterialOverride : completedMaterialDefault;

        if (gameObject.tag == "Player")
        {
            setMaterial(shapeMaterial);
        }
    }

    void Update()
    {
        if (lerpingBetweenMaterials)
        {
            lerpTime += Time.deltaTime;
            lerp = lerpTime / lerpDuration;
            if (lerp < 1)
            {
                GetComponent<Renderer>().material.Lerp(lerpMaterialStart, lerpMaterialEnd, lerp);
            }
            else
            {
                GetComponent<Renderer>().material = lerpMaterialEnd;
                lerpTime = 0;
                lerpingBetweenMaterials = false;
            }
        }

    }

    public void startLevelComplete()
    {
        lerpBetweenMaterials(GetComponent<Renderer>().material, completedMaterial, lerpDuration);

    }

    // ---------------
    // Material Transitions
    // ---------------

    public void lerpBetweenMaterials(Material mat1, Material mat2, float duration)
    {
        lerpingBetweenMaterials = true;
        lerpMaterialStart = mat1;
        lerpMaterialEnd = mat2;
        lerpDuration = duration;
        lerpTime = 0;
        lerp = 0;
    }


    public void fadeToAlpha(float alpha, float duration)
    {
      
        StartCoroutine(FadeTo(alpha, duration));

    }

    IEnumerator FadeTo(float alphaFinal, float aTime)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        Color newColor = GetComponent<Renderer>().material.color;


        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {

            newColor.a = Mathf.Lerp(alpha, alphaFinal, t);

            GetComponent<Renderer>().material.SetColor("_Color", newColor);

            yield return null;
        }
        Destroy(gameObject);
    }

    // ---------------
    // Helper Functions
    // ---------------

    public void setMaterial(Material newMaterial)
    {
        GetComponent<Renderer>().material = newMaterial;
    }

    public void setMaterialAlpha(Material newMaterial, float alpha)
    {
        GetComponent<Renderer>().material = newMaterial;
        Color color = GetComponent<Renderer>().material.color;
        color.a = alpha;
    }


}

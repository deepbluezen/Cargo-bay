using System;
using System.Collections.Generic;
using UnityEngine;

/* [ExecuteInEditMode]
[ExecuteAlways] */

public class SetRSUV : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

           MeshRenderer[] allMeshRenderers = FindObjectsByType<MeshRenderer>(FindObjectsSortMode.InstanceID);
        foreach (MeshRenderer meshRenderer in allMeshRenderers)
        {
            // compute a random color
            Color32 c = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0.6f, 1.0f), UnityEngine.Random.Range(0.6f, 1.0f));

            // set it as a LDR color in the RSUV value of the renderer
            uint cc = ((uint)c.b << 16) | ((uint)c.g << 8) | ((uint)c.r << 0);
            meshRenderer.SetShaderUserValue(cc);
        }
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

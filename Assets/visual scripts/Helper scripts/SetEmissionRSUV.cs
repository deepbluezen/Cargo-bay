using System;
using System.Runtime.CompilerServices;
using UnityEngine;


[ExecuteInEditMode]
[ExecuteAlways]

public class SetEmissionRSUV : MonoBehaviour
{
    
    public enum VaryIntensity
    {
        Steady,Variable
    }
    
    public enum VaryColour
    {
        One,Many
    }
    
    public enum ColourPallette
    {
        Vertex,Pallete
    }
    
    public VaryIntensity varyIntensity = VaryIntensity.Steady;
    public VaryColour varyColour = VaryColour.One;
    [Range(0,15)]
    public int Intensity = 0;
    public ColourPallette colourPallette = ColourPallette.Pallete;
    [Range(0,7)]
    public int UVselect = 0;
    [Range(0,15)]
    public int IntensityTimeScale = 0;
    [Range(0,15)]
    public int IntensityTimeOffset = 0;
    [Range(0,15)]
    public int ColourTimeScale = 0;
    [Range(0,15)]
    public int ColourTimeOffset = 0;

    void Start()
    {
       UpdateData();  
    }

    void Update()
    {
         UpdateData();
    }


    void UpdateData()
    {
       
        uint data = 0x00000000; // All bits set to 0
        MeshRenderer meshwithRSUVset;

        data = HelpersRSUV.EncodeData(data, (int)varyIntensity, 0, 1);
        data = HelpersRSUV.EncodeData(data, (int)varyColour, 1, 1);
        data = HelpersRSUV.EncodeData(data, (int)Intensity, 2, 4);
        data = HelpersRSUV.EncodeData(data, (int)colourPallette, 6, 1);
        data = HelpersRSUV.EncodeData(data, (int)UVselect, 7, 3);
        data = HelpersRSUV.EncodeData(data, (int)IntensityTimeScale, 10, 4);
        data = HelpersRSUV.EncodeData(data, (int)IntensityTimeOffset, 14, 4);
        data = HelpersRSUV.EncodeData(data, (int)ColourTimeScale, 18, 4);
        data = HelpersRSUV.EncodeData(data, (int)ColourTimeOffset, 22, 4);
        meshwithRSUVset = gameObject.GetComponent<MeshRenderer>();
        meshwithRSUVset.SetShaderUserValue(data);
       
        uint setRSUV =  meshwithRSUVset.GetShaderUserValue();
        Debug.Log(setRSUV);

        
    }
}

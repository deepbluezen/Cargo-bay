using System;
using System.Runtime.CompilerServices;
using UnityEngine;


[ExecuteInEditMode]
[ExecuteAlways]
public class SetIndividualRSUV : MonoBehaviour
{
    public enum MainPallete
    {
        //need to extend this / align with colours end up with
        Grey,Orange,Blue,Red,Green,extra1, extra2
    }
    public enum HighlightPallete
    {
        Grey,Orange,Blue,Red,Green, extra1, extra2
    }

    public enum MeshOffsetAxis
    {
        none,x,y,z
    }

    public enum HideMeshPart
    {
        none, some,somemore, all 
    } 
    
    public enum MeshRotation
    {
        None=1,Rotate=0
    }

    public enum Emission
    {
        None,Shiny
    }

    public HighlightPallete highlightPallete = HighlightPallete.Orange;
    public MainPallete mainPallete = MainPallete.Grey;
    public HideMeshPart hideMeshPart  = HideMeshPart.none;
    public MeshRotation meshRotation = MeshRotation.None;
    [Range(0,15)]
    public int MeshRotationMagnitude = 0;
    public MeshOffsetAxis meshoffsetAxis = MeshOffsetAxis.x;
    [Range(0,31)]
    public int MeshOffsetMagnitude = 0;
    public Emission emission = Emission.None;
    [Range(0,7)]
    public int Smoothness = 0;
    private int Random = 0;

    

    void Start()
    {
         UpdateData();
    }

       void OnValidate()
    {
         UpdateData();
    }

void UpdateData()
    {
        this.Random = UnityEngine.Random.Range(0,255);
        uint data = 0x00000000; // All bits set to 0
        MeshRenderer meshwithRSUVset;

        data = HelpersRSUV.EncodeData(data, (int)highlightPallete, 0, 3);
        data = HelpersRSUV.EncodeData(data, (int)mainPallete, 3, 3);
        data = HelpersRSUV.EncodeData(data, (int)hideMeshPart, 6, 2);
        data = HelpersRSUV.EncodeData(data, (int)meshRotation, 8, 1);
        data = HelpersRSUV.EncodeData(data, (int)MeshRotationMagnitude, 9, 4);
        data = HelpersRSUV.EncodeData(data, (int)meshoffsetAxis, 13, 2);
        data = HelpersRSUV.EncodeData(data, (int)MeshOffsetMagnitude, 15, 5);
        data = HelpersRSUV.EncodeData(data, (int)emission, 20, 1);
        data = HelpersRSUV.EncodeData(data, (int)Smoothness, 21, 3);
        data = HelpersRSUV.EncodeData(data, (int)Random, 24, 8);
        meshwithRSUVset = gameObject.GetComponent<MeshRenderer>();
        meshwithRSUVset.SetShaderUserValue(data);
       
        uint setRSUV =  meshwithRSUVset.GetShaderUserValue();
        Debug.Log(setRSUV);

        
    }

}

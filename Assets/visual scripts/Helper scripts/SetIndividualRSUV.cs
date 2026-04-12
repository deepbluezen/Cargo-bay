using System;
using UnityEngine;


[ExecuteInEditMode]
[ExecuteAlways]
public class SetIndividualRSUV : MonoBehaviour
{
    public enum MainPallete
    {
        Grey,Orange,Blue,Red,Green
    }
    public enum HighlightPallete
    {
        Grey,Orange,Blue,Red,Green
    }

    public enum MeshOffsetAxis
    {
        x,y,z
    }

    public enum HideMeshPart
    {
        none, some,somemore, all 
    } 
    


    public HighlightPallete highlightPallete = HighlightPallete.Orange;
    public MainPallete mainPallete = MainPallete.Grey;
    public HideMeshPart hideMeshPart  = HideMeshPart.none;
    public bool MeshRotation = false;
    [Range(0,15)]
    public int MeshRotationMagnitude = 0;
    public MeshOffsetAxis meshoffsetAxis = MeshOffsetAxis.x;
    [Range(0,31)]
    public int MeshOffsetMagnitude = 0;
    public bool Emission = false;
    [Range(0,7)]
    public int Smoothness = 0;

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
        uint data = 0x00000000; // All bits set to 0
        
        data = HelpersRSUV.EncodeData(data, (int)highlightPallete, 0, 3);
        

        //only seem to be able to set RSUV not get from meshrenderer in c#
        //so log out value are setting 
        Debug.Log(data);
    }

}

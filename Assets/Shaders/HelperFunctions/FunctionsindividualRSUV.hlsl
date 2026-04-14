// From Unity RSV URP sample

#include "HelpersRSUV.hlsl"

//can group outputs where its going to make sense to be pulling from one node in graph
void GetPallete_float(out float MainPallete, out float HighlightPallete )
{
     uint data = GetData();
     MainPallete = DecodeBitsToInt(data,3,3)/5;
     HighlightPallete = DecodeBitsToInt(data,0,3)/5;
}

void GetRSUVHideMeshPart_float(out float HideMeshPart)
{
    uint data = GetData();
    HideMeshPart = DecodeBitsToInt(data,6,2)/4;
   
}

void GetRSUVMeshRotation_float(out float MeshRotation,out float MeshRotationMagnitude)
{
     uint data = GetData();
     MeshRotation = GetBit(data,8);
     MeshRotationMagnitude = DecodeBitsToInt(data,9,4);
}

void GetRSUVEmissionSmoothness_float(out float RSUVEmission, out float RSUVSmoothness)
{
    uint data = GetData();
    RSUVEmission = GetBit(data,20);
    RSUVSmoothness = DecodeBitsToInt(data,21,3)/7;
}

void GetRSUVMeshoffset_float(out float MeshoffsetAxis, out float MeshoffsetMagnitude)
{
    uint data = GetData();
    MeshoffsetAxis = DecodeBitsToInt(data,13,2);
    MeshoffsetMagnitude = DecodeBitsToInt(data,15,5)/31;
}

/* void GetRSUVHideMeshPart_bool(out bool HideMeshPart)
{
    uint data = GetData();
    //initially done as boolean not multiple levels
    //HideMeshPart = DecodeBitsToInt(data,6,2)// change out to float
    HideMeshPart = GetBit(data,6);
} */

/* 
void GetColor_float(out float4 Color)
{
    uint data = GetData();
    Color = DecodeUintToFloat4(data);
}

void GetOffset_float(out float Offset)
{
    uint data = GetData();
    Offset = DecodeBitsToInt(data,0,2);
} */

/* From URP sample as reference for how to pull out ranges and work with values
void GetRendererShaderUserValueHeadGear_float(out float HeadGear)
{
    // Default is none => 0
    uint data = GetData();
    float rawHeadGear = DecodeBitsToInt(data, 18, 2);
    HeadGear = rawHeadGear == 0 ? 0 : 0.95 - ((rawHeadGear - 1) * 0.1);
}

void GetRendererShaderUserValueBody_float(out float BellySize, out float SkinColor, out float ClothColor)
{
    uint data = GetData();
    SkinColor = DecodeBitsToInt(data, 0, 3) / 5; //There's 5 different skin color
    ClothColor = DecodeBitsToInt(data, 3, 3) / 7; //There's 7 different cloth color
    BellySize = DecodeBitsToInt(data, 6, 2) / 3; //Encoded in 8 steps
}
*/


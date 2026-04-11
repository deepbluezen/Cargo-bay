// From Unity RSV URP sample

#include "HelpersRSUV.hlsl"


void GetColor_float(out float4 Color)
{
    uint data = GetData();
    Color = DecodeUintToFloat4(data);
}


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


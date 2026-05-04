#include "HelpersRSUV.hlsl"

void GetVaryIntensity_float(out float VaryIntensity)
{
    uint data = GetData();
    VaryIntensity = GetBit(data,0);
}

void GetVaryColour_float(out float VaryColour)
{
    uint data = GetData();
    VaryColour = GetBit(data,0);
}

void GetIntensity_float(out float Intensity)
{
    uint data = GetData();
    Intensity = DecodeBitsToInt(data,2,4)*2;
}

void GetColourPalette_float(out float ColourPalette)
{
    uint data = GetData();
    ColourPalette = GetBit(data,6);
}

void GetUVSelect_float(out float UVselect)
{
    uint data = GetData();
    UVselect = DecodeBitsToInt(data,7,3)/7;
}

void GetIntensityTime_float(out float IntensityTimeScale, out float IntensityOffset)
{
    uint data = GetData();
    IntensityTimeScale = DecodeBitsToInt(data,10,4)/7;
    IntensityOffset = DecodeBitsToInt(data,14,4)/15;
}

void GetColourTime_float_float(out float ColourTimeScale, out float ColourTimeOffset)
{
    uint data = GetData();
    ColourTimeScale = DecodeBitsToInt(data,18,4)/15;
    ColourTimeOffset = DecodeBitsToInt(data,22,4)/15;
}
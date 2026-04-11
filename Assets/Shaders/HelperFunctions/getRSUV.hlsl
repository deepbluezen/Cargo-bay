uint c= 42u;

float4 mycolour = 1;
float4 mycolor = float4((float)((c >> 0) & 255) * (1.f / 255.f),
                        (float)((c >> 8) & 255) * (1.f / 255.f),
                        (float)((c >> 16) & 255) * (1.f / 255.f),
                        (float)1.f);

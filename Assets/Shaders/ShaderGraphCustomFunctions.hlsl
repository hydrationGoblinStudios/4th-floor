void QuantizeScreenPos_float(float4 pos, out float4 outPos)
{
    outPos.x = floor(pos.x*640)/640;
    outPos.y = floor(pos.y*360)/360;
    outPos.zw = pos.zw;
}
void QuantizeScreenPos_half(half4 pos, out float4 outPos)
{
    outPos.x = floor(pos.x*640)/640;
    outPos.y = floor(pos.y*360)/360;
    outPos.zw = pos.zw;
}
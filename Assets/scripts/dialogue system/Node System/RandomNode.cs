using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class RandomNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    [Output] public int exit2;
    [Output] public int exit3;
    [Output] public int exit4;
    public int Saidas;
    public override string GetString()
    {

        int random = Random.Range(1, Saidas +1);
       
        return "RandomNode/" + random;
    }
}

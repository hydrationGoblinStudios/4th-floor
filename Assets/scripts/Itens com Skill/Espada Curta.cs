using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaCurta : Item
{
    public bool Boost;
    public override int ItemProc(int damage)
    {
        if (!Boost)
        {
            holder.avoid += 15;
            Boost = true;
        }

        return 0;
    }







}

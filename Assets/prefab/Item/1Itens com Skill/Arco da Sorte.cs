using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcodaSorte : Item
{
    public bool Boost;
    public override int ItemProc(int damage)
    {
        if (!Boost & Random.Range(0, 101) <= holder.luck)
        {
            holder.power += 5;
            holder.hit += 5;
            holder.crit += 5;
            Boost = true;
        }

        return 0;
    }







}

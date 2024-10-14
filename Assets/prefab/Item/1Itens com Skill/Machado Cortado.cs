using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachadoCortado : Item
{
    public bool Boost;
    public override int ItemProc(int damage)
    {
        if (holder.hp < holder.maxhp*0.8)
        {
            holder.hit += 20;
            holder.crit += 10;
        }

        return 0;
    }







}

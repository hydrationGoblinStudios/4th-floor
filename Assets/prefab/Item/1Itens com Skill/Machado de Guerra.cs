using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachadodeGuerra : Item
{
    public bool Boost;
    public override int ItemProc(int damage)
    {
        if (!Boost)
        {

            holder.speed -= (float)(holder.speed * 0.2);
            Boost = true;
        }

        return 0;
    }







}

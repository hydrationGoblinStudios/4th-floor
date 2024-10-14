using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivroArriscado : Item
{
    public bool Boost;
    public override int ItemProc(int damage)
    {
        if (!Boost)
        {
            holder.soulgain += 5;
            Boost = true;
        }

        return 0;
    }







}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feitiçeiro : UnitBehavior
{
    public override int Soul(int damage)
    {
        return (int)((damage / 3 + 1) + (enemyBehavior.def*0.35));
    }
    public override int Proc(int damage)
    {
        //placeholder exp *1.4
        //homer simpson
        return 1;
    }
}
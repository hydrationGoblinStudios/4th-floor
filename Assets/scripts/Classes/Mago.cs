using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : UnitBehavior
{
    public override int Soul(int damage)
    {
        return damage / 2 + 1 + (enemyBehavior.def/2);
    }
    public override int Proc(int damage)
    {
        if (enemy)
        {
            battleManager.Esoul += 1;
        }
        else
        {
            battleManager.Psoul += 1;
        }
        return 0;
    }
}
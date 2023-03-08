using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feitiçeiro : UnitBehavior
{
    public override int Soul(int damage)
    {
        return damage / 3 + 1 + enemyBehavior.def / 4;
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
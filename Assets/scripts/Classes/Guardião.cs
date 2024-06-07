using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardião : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            enemyBehavior.def += 8;
            battleManager.StatChange();
            return damage * 4;
        }
        else
        {
            playerBehavior.def += 8;
            battleManager.StatChange();
            return damage * 4;

        }
    }

    //statchance só no ataque talvez consertar
    public override int Proc(int damage)
    {
        str += def / 3;
        battleManager.StatChange();

        return 0;
    }
}

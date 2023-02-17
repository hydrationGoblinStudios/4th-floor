using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueiro : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            enemyBehavior.dex += 10;
            battleManager.StatChange();

        }
        else
        {
            playerBehavior.dex += 10;
            battleManager.StatChange();
        }

        return 0;
    }

    public override int Proc(int damage)
    {
        //prescisa ser consertado
        if (hit > 100)
        {
            return damage + ((atk / 100) * (hit - 100));
        }
        return 0;
    }

}

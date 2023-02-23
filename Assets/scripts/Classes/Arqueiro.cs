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
        return battleManager.Phit / 10;
    }

}


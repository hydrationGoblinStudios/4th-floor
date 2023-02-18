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
        if (hit > 100 & enemy)
        {

            return battleManager.Edamage * (battleManager.Ehit - 100) / 100;
        }
        else if (hit > 100 & !enemy)
        {
            return battleManager.Pdamage * (battleManager.Phit - 100)/100;
        }
        return 0;
    }

}


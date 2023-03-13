using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocultista : UnitBehavior
{
    public override int Soul(int damage)
    {
        //prescisa ser consertado
        if (enemy)
        {
            playerBehavior.luck -= 7;
            playerBehavior.dex -= 7;
            playerBehavior.def -= 3;
            enemyBehavior.hp += (maxhp / 10);
            battleManager.StatChange();
            return 0;
        }
        else
        {
            enemyBehavior.luck -= 7;
            enemyBehavior.dex -= 7;
            enemyBehavior.def -= 3;
            playerBehavior.hp += (maxhp / 10);
            battleManager.StatChange();
            return 0;
        }
    }
    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) < dex * 2)
        {
            if (enemy)
            {
                enemyBehavior.luck += 5;
                enemyBehavior.dex += 5;
                battleManager.StatChange();
                battleText.text = "Hora Da bazinga(eviL)";
                return 0;
            }
            else
            {
                playerBehavior.luck += 5;
                playerBehavior.dex += 5;
                battleManager.StatChange();
                battleText.text = "Hora Da bazinga";
                return 0;
            }

        }
        return 0;
    }
}
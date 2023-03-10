using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alquimista : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            playerBehavior.luck -= 5;
            playerBehavior.dex -= 5;
            playerBehavior.def -= 2;
            battleManager.StatChange();
            return 0;
        }
        else
        {
            enemyBehavior.luck -= 5;
            enemyBehavior.dex -= 5;
            enemyBehavior.def -= 2;
            battleManager.StatChange();
            return 0;
        }
    }
    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) < dex*2)
        {
            if (enemy)
            {
                enemyBehavior.luck += 5;
                enemyBehavior.dex += 5;
                battleManager.StatChange();
                battleText.text = "Hora Da bazinga";
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

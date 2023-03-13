using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavaleiro : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            enemyBehavior.def += 5;
            battleManager.StatChange();
            return damage * 3;
        }
        else
        {
            playerBehavior.def += 5;
            battleManager.StatChange();
            return damage * 3;

        }
    }


    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) < dex)
        {
            if (enemy)
            {
                battleText.text = "Soco dos DEMONIOS";
                return playerBehavior.def / 2;
            }
            else
            {
                battleText.text = "Soco dos deuses";
                return enemyBehavior.def / 2;
            }
        }
        return 0;
    }
}

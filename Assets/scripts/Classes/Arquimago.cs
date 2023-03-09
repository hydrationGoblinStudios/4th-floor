using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arquimago : UnitBehavior
{
    public override int Soul(int damage)
    {

        atk += damage / 10;
        return (int)(damage + (enemyBehavior.def * 0.75));
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
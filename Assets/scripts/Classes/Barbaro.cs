using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbaro : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            enemyBehavior.speed += 1;

        }
        else
        {
            playerBehavior.speed += 1;
        }
        return 0;
    }
    //não finalizado
    public override int Proc(int damage)
    {
        if (enemy && enemyBehavior.atk > playerBehavior.atk)
        {
            hp += damage / 10;

        }
        if (!enemy && playerBehavior.atk > enemyBehavior.atk)
        {
            hp += damage / 10;
        }
        return 0;

    }
}
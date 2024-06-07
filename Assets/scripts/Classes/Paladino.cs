using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladino : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.PlayerBar -= 50;
            enemyBehavior.str += 10;
        }
        else
        {
            battleManager.EnemyBar -= 50;
            playerBehavior.str += 10;
        }
        hp += (maxhp / 100) * luck;
        if (hp >= maxhp)
        {
            hp = maxhp;
        }
        return damage / 2 + 1;
    }
    public override int Proc(int damage)
    {
        return luck/2;
    }
}

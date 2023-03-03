using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiador : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Super golpe"));
            battleManager.PlayerBar -= 50;
            battleManager.StatChange();
        }
        else
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Super Golpe"));
            battleManager.EnemyBar -= 50;
            battleManager.StatChange();
        }

        return 0;
    }
    public override int Proc(int damage)
    {

        if (battleManager.enemyBehavior.hp < (battleManager.enemyBehavior.maxhp / 2))
        {
            crit += 15;
            battleManager.StatChange();
            return 0;
        }
        return 0;
    }
}

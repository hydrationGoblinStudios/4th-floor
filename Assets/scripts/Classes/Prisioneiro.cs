using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisioneiro : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Super golpe"));
            battleManager.PlayerBar -= 50;
        }
        else
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Super Golpe"));
            battleManager.EnemyBar -= 50;

        }

        return 0;
    }
    public override int Proc(int damage)
    {
        speed += (maxhp - hp) / 10;
        battleManager.StatChange();
        return 0;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humano : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Super golpe"));
            battleManager.PlayerBar -= 50;
            battleManager.StatChange();
            crit += 100;
            StartCoroutine(battleManager.PlayerExtraAttack("Super golpe"));
            crit -= 100;

            return enemyBehavior.def / 2;
        }
        else
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Super golpe"));
            battleManager.EnemyBar -= 50;
            battleManager.StatChange();
            crit += 100;
            StartCoroutine(battleManager.PlayerExtraAttack("Super golpe"));
            crit -= 100;
            return playerBehavior.def / 2;
        }
    }

    public override int Proc(int damage)
    {


        if (battleManager.playerBehavior.Pendure == true && hp <= 0)
        {
            hp = 1;
            battleManager.Psoul += 3;
            battleManager.StatChange();
            Pendure = false;
        }
        return 0;
    }
}


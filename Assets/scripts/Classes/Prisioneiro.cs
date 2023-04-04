using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisioneiro : UnitBehavior
{
    public string descriptionA = "O prisioneiro golpeia 2 vezes, um recupera parte da vida, e o outro reduz a barra de alma";
    public string descriptionP = "Quanto menos vida mais rapido o Prisioneiro vai atacar.";
    public override int Soul(int damage)
    {
        if (enemy)
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Alma errante"));
            battleManager.PlayerBar -= 50;
        }
        else
        {
            hp += damage / 2;
            StartCoroutine(battleManager.PlayerExtraAttack("Alma errante"));
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


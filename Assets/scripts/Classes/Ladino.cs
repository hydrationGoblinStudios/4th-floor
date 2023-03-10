using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladino : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.Eskill = -(battleManager.Edamage / 2);
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            enemyBehavior.dex += 15;
            battleManager.StatChange();
            return 0;
        }
        else
        {
            battleManager.Pskill = -(battleManager.Pdamage / 2);
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            playerBehavior.dex += 15;
            battleManager.StatChange();
            return 0;
        }

    }
    public override int Proc(int damage)
    {
        if (damage != 0)
        {
            avoid += 2;
            battleManager.StatChange();
            return 0;
        }
        else
        {
            return 0;
        }
    }
}

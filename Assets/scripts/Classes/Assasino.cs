using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasino : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.Eskill = -(battleManager.Edamage / 2);
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));

            enemyBehavior.dex += 20;
            battleManager.StatChange();
        }
        else
        {
            battleManager.Pskill = -(battleManager.Pdamage / 2);
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            playerBehavior.dex += 20;
            battleManager.StatChange();
        }

        return 0;
    }
    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) < dex/4)
        {
            return damage + 9999;

        }
        return 0;
    }
}

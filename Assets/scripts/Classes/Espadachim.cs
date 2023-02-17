using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espadachim : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.Eskill = -(battleManager.Edamage / 2);
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger do mal moment"));
        }
        else
        {
            battleManager.Pskill = -(battleManager.Pdamage/2);
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
            StartCoroutine(battleManager.PlayerExtraAttack("rutger moment"));
        }

        return 0 ;
    }
    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) > dex)
        {
            if (enemy)
            {
                StartCoroutine(battleManager.PlayerExtraAttack("astra moment"));
            }
            else
            {
                StartCoroutine(battleManager.PlayerExtraAttack("astra moment"));
            }
        }
        return 0;
    }
}

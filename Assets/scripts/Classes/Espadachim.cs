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
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rapidos!"));
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rapidos!"));
            battleManager.StatChange();
        }
        else
        {
            battleManager.Pskill = -(battleManager.Pdamage/2);
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rapidos!"));
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rapidos!"));
            battleManager.StatChange();
        }

        return 0 ;
    }
    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) < dex)
        {
            if (enemy)
            {
                StartCoroutine(battleManager.PlayerExtraAttack("Ataque Duplo ativou!"));
            }
            else
            {
                StartCoroutine(battleManager.PlayerExtraAttack("Ataque Duplo ativou!"));
            }
        }
        return 0;
    }
}

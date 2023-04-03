using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espadachim : UnitBehavior
{
    public string descriptionA = "O Espadachim ataca 2 vezes";
    public string descriptionP = "Uma chance baseada na destreza em atacar 2 vezes";

    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.Eskill = -(battleManager.Edamage / 2);
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rápidos!"));
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rápidos!"));
            battleManager.StatChange();
        }
        else
        {
            battleManager.Pskill = -(battleManager.Pdamage/2);
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rápidos!"));
            StartCoroutine(battleManager.PlayerExtraAttack("Ataques Rápidos!"));
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

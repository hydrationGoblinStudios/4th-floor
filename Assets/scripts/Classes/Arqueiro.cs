using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueiro : UnitBehavior
{
    public string descriptionA = "O Arqueiro faz um golpe que aumenta a destreza em 10";
    public string descriptionP = "Ganha mais dano baseado na chance de acerto";
    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.battleText.text = "Golpe Presciso!";
            enemyBehavior.dex += 10;
            battleManager.StatChange();

        }
        else
        {
            battleManager.battleText.text = "Golpe Presciso!";
            playerBehavior.dex += 10;
            battleManager.StatChange();
        }

        return 0;
    }

    public override int Proc(int damage)
    {
        return battleManager.Phit / 30;
    }

}


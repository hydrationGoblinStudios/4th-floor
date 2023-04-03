using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbaro : UnitBehavior
{
    public string descriptionA = "O Barbaro ataca mais rapido pelo resto da batalha";
    public string descriptionP = "Se o barbaro for mais forte que o oponente ele ira se curar em parte do dano";

    public override int Soul(int damage)
    {
        if (enemy)
        {
            enemyBehavior.speed += 1;

        }
        else
        {
            playerBehavior.speed += 1;
        }
        return 0;
    }
    public override int Proc(int damage)
    {
        if (enemy && enemyBehavior.atk > playerBehavior.atk)
        {
            hp += damage / 10;

        }
        if (!enemy && playerBehavior.atk > enemyBehavior.atk)
        {
            hp += damage / 10;
        }
        return 0;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbaro : UnitBehavior
{
    public bool Endure;
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
    //não finalizado
    public override int Proc(int damage)
    {
        Endure = true;
        if (battleManager.Pdamage >= hp && Endure == true)
        {
            hp = 1;
            Endure = false;
        }
        return 0;
    }
    }
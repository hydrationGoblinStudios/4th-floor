using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbaro : UnitBehavior
{
    public bool EndureBool;
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
    public void Endure()
    {
        if (EndureBool)
        {
            hp = 1;
            EndureBool = false;
        }
   
    }

}
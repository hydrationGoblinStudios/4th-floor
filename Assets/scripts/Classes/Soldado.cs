using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : UnitBehavior
{
    public override int Soul(int damage)
    {
        if(enemy)
        {
            battleManager.PlayerBar -= 50;
        }
        else
        {
            battleManager.EnemyBar -= 50;
        }
        return damage/4 +1;
    }
    public override int Proc(int damage)
    {
        if(Random.Range(0,101) < dex)
        {
            battleManager.battleText.text = "Pancada Gamer";
            return hp / 4 + 1;
        }
        return 0;
    }
}
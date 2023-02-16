using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : UnitBehavior
{
   public override int Soul(int damage)
    {
        hp += damage;
        battleText.text = "drain";
        if(hp >= maxhp)
        {

            hp = maxhp;
        }
        return 0;
    }
    public override int Proc(int damage)
    {
        if(damage != 0)
        {
            return 0;
        }
        else
        {
            speed += 1;
            battleManager.StatChange();
            return 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerreiro : UnitBehavior
{
   public override int Soul(int damage)
    {
        return damage * 2;
    }
    public override int Proc(int damage)
    {
        if (hp <= (maxhp / 4))
        {
            battleText.text = "rage proc";
            return damage/3;
        }
        return 0;
    }
}

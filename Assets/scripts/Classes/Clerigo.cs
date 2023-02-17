using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clerigo : UnitBehavior
{
    public override int Soul(int damage)
    {
        if (enemy)
        {
            battleManager.PlayerBar -= 50;
        }
        else
        {
            battleManager.EnemyBar -= 50;
        }
        hp += (maxhp / 100) * luck;
        if (hp >= maxhp)
        {
            hp = maxhp;
        }
        return damage /2 + 1;
    }
    public override int Proc(int damage)
    {
        if (Random.Range(0, 101) < dex)
        {
            battleManager.battleText.text = "Pancada Super Gamer";
            hp += damage / 2;
            if (hp >= maxhp)
            {
                hp = maxhp;
            }
        }
        return 0;
    }
}

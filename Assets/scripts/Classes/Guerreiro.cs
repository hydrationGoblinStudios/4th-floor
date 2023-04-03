using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerreiro : UnitBehavior
{
    public string descriptionA = "O Gurreiro golpeia com um golpe que da o dobro de dano";
    public string descriptionP = "Com 1/4 do hp, o Guerreiro vai causar mais dano";
    public override int Soul(int damage)
    {
        battleManager.battleText.text = "Golpe devastador!";
        return damage * 2;
    }
    public override int Proc(int damage)
    {
        if (hp <= (maxhp / 4))
        {
            battleText.text = "Furia ativou!";
            return damage/3;
        }
        return 0;
    }
}

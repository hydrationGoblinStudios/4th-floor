using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerreiro : UnitBehavior
{
    public void Awake()
    {
        baseSkill = "Lutador Vers�til";
        skill1 = "Dur�o";
        baseSoul = "Golpe Poderoso";
        soul1 = "Revigoramento";
        UsableWeaponTypes = new() { Item.Weapontype.Lance };

    }

}

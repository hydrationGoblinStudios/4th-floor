using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerreiro : UnitBehavior
{
    public void Awake()
    {
        baseSkill = "Lutador Versátil";
        skill1 = "Durão";
        baseSoul = "Golpe Poderoso";
        soul1 = "Revigoramento";
        UsableWeaponTypes = new() { Item.Weapontype.Axe };

    }

}

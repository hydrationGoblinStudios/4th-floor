using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerreiro : UnitBehavior
{
    public Guerreiro()
    {
        currentRank = 1;
        ClassGrowths = new() { 5, 5, 0, 0, 0, 0, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Lutador Versátil";
        skill1 = "Durão";
        baseSoul = "Golpe Poderoso";
        soul1 = "Revigoramento";
        UsableWeaponTypes = new() { Item.Weapontype.Axe };
        ClassGrowths = new() { 5, 5, 0, 0, 0, 0, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };

    }

}

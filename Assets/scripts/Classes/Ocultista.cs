using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocultista : UnitBehavior
{
    public Ocultista()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 15, 0, 15, 0, 10, -10 };
        classStats = new() { 0, 0, 3, 0, 2, 0, 2,-2 };
    }

    public void Awake()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Bow };
        ClassGrowths = new() { 0, 0, 15, 0, 15, 0, 10, -10 };
        classStats = new() { 0, 0, 3, 0, 2, 0, 2, -2 };
    }
}
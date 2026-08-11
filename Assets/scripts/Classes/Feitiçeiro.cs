using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feitiçeiro : UnitBehavior
{
    public Feitiçeiro()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 0, 0, 5, 0, 0, 5 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Concentração de feiticeiro";
        skill1 = "Magia Destrutiva";
        baseSoul = "Sabedoria Arcana";
        soul1 = "Trovoada";
        UsableWeaponTypes = new() { Item.Weapontype.Tome };
        ClassGrowths = new() { 0, 0, 0, 0, 5, 0, 0, 5 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public override void InitClass()
    {
        baseSkill = "Concentração de feiticeiro";
        skill1 = "Magia Destrutiva";
        baseSoul = "Sabedoria Arcana";
        soul1 = "Trovoada";
        UsableWeaponTypes = new() { Item.Weapontype.Tome };
        ClassGrowths = new() { 0, 0, 0, 0, 5, 0, 0, 5 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
}
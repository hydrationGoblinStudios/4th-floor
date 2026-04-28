using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbaro : UnitBehavior
{
    public Barbaro()
    {
        currentRank = 1;
        ClassGrowths = new() { 20, 20, -10, 5, 5, 0, 0, 0 };
        classStats = new() { 15, 3, -1, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Axe};
        ClassGrowths = new List<int> { 20, 20, -10, 0, 0, 0, 0, 0 };
        classStats = new List<int> { 15, 3, -1, 0, 0, 0, 0, 0 };
    }
    public override void InitClass()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Axe };
        ClassGrowths = new List<int> { 20, 20, -10, 0, 0, 0, 0, 0 };
        classStats = new List<int> { 15, 3, -1, 0, 0, 0, 0, 0 };
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueiro : UnitBehavior
{
   public Arqueiro()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 0, 5, 5, 0, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
        InitClass();
    }
    public override void InitClass()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Bow };
        ClassGrowths = new List<int> { 10, 10, 0, 5, 0, 15, -10, 0 };
        classStats = new List<int> { 10, 1, 0, 1, -2, 4, -3, 0 };
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanceiro : UnitBehavior
{
    public Lanceiro()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 0, 10, 10, 0, 0, 10 };
        classStats = new() { 0, 0, 0, 2, 3, 0, 0, 0 };
    }

    public void Awake()
    {
        InitClass();
    }
    public override void InitClass()
    {
        classSkill = "Penetrar Defesas";
        baseSkill = "Lança Perfurante";
        skill1 = "Ritmo Crítico";
        baseSoul = "Perfurar";
        soul1 = "Jogar Lança";
        UsableWeaponTypes = new() { Item.Weapontype.Lance};
        ClassGrowths = new List<int> { 0, 0, 0, 10, 10, 0, 0, 10 };
        classStats = new List<int> { 0, 0, 0, 2, 3, 0, 0, 0 };
    }
}
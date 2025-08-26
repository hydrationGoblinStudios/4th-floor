using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladino : UnitBehavior
{ 
    public Paladino()
    {
        currentRank = 1;
        ClassGrowths = new () { 5, 5, 5, 5, 5, 0, 0, 5 };
        classStats = new() { 5, 1, 1, 1, 1, 0, 0, 0 };
    }
    public void Awake()
{
    baseSkill = "Foco";
    skill1 = "Precisão Mortal";
    baseSoul = "Tiro Certeiro";
    soul1 = "Rajada de Flechas";
    UsableWeaponTypes = new() { Item.Weapontype.Lance, Item.Weapontype.Tome };
    ClassGrowths = new List<int> { 5, 0, 0, 0, 0, 0, 10, 15 };
    classStats = new List<int> { 5, 0, 0, 0, 0, 0, 2, 2 };
}
}

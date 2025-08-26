using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duelista: UnitBehavior
{
public Duelista()
    {
        currentRank = 1;
        ClassGrowths = new() { 5, 5, 5, 5, 5, 0, 0, 5 };
        classStats = new() { 5, 1, 1, 1, 1, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Sword };
        ClassGrowths = new List<int> { 5, 5, 0, 10, 10, 0, 0, 0 };
        classStats = new List<int> { 0, 1, 0, 2, 2, 0, 0, 0 };
    }
}
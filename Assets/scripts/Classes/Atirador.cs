using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador : UnitBehavior
{
    public Atirador()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 5, 0, 10, 10, 0, 5, 0 };
        classStats = new() { 0, 1, 0, 3, 1, 0, 0, 0 };
    }

    public void Awake()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Sword,Item.Weapontype.Bow };
        ClassGrowths = new List<int> { 0, 0, 0, 10, 10, 0, 0, 10 };
        classStats = new List<int> { 0, 0, 0, 2, 3, 0, 0, 0 };
    }
}
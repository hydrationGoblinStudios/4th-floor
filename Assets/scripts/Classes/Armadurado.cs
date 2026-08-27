using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadurado : UnitBehavior
{
    public Armadurado()
    {
        currentRank = 1;
        ClassGrowths = new List<int> { 10, 10, 0, 5, 0, 15, -10, 0 };
        classStats = new List<int> { 10, 1, 0, 1, -2, 4, -3, 0 };
    }
    public void Awake()
    {
        InitClass();
    }
    public override void InitClass()
    {
        baseSkill = "Foco";
        skill1 = "Precis�o Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Axe, Item.Weapontype.Lance};
        ClassGrowths = new List<int> { 10, 10, 0, 5, 0, 15, -10, 0 };
        classStats = new List<int> { 10, 1, 0, 1, -2, 4, -3, 0 };
    }
}
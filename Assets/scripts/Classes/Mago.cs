using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : UnitBehavior
{
    public Mago()
    {
        currentRank = 1;
        ClassGrowths = new() { 5, 5, 5, 5, 5, 0, 0, 5 };
        classStats = new() { 5, 1, 1, 1, 1, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Presença Inabalável";
        skill1 = "Pancada";
        baseSoul = "Golpe Atordoante";
        soul1 = "Fortificar";
        UsableWeaponTypes = new() { Item.Weapontype.Tome };
        ClassGrowths = new List<int> { -5, -5, 15, 0, 10, 0, 10, 5 };
        classStats = new List<int> { -10, -1, 4, 0, 2, 0, 2, 0 };
    }
}
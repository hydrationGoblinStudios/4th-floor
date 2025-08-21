using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : UnitBehavior
{
    public Soldado()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 0, 5, 0, 5, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
    baseSkill = "Presença Inabalável";
    skill1 = "Pancada";
    baseSoul = "Golpe Atordoante";
    soul1 = "Fortificar";
    UsableWeaponTypes = new() { Item.Weapontype.Lance };
        ClassGrowths = new() { 0, 0, 0, 5, 0, 5, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
}
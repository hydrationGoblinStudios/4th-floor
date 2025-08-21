using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Místico : UnitBehavior
{
    public Místico()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 5, 0, 0, 0, 5, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Encantamento";
        skill1 = "Maldição";
        baseSoul = "Restauração Espiritual";
        soul1 = "Benção dos Ventos";
        UsableWeaponTypes = new() { Item.Weapontype.Receptacle };
        ClassGrowths = new() { 0, 0, 5, 0, 0, 0, 5, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curandeiro : UnitBehavior
{
    public void Awake()
    {
        baseSkill = "Foco";
        skill1 = "Precisão Mortal";
        baseSoul = "Tiro Certeiro";
        soul1 = "Rajada de Flechas";
        UsableWeaponTypes = new() { Item.Weapontype.Tome,Item.Weapontype.Receptacle };
        ClassGrowths = new List<int> { 0, -5, 10, 0, 0, 0, 15, 10 };
        classStats = new List<int> { 0, -1, 3, 0, 0, 0, 3, 0 };
    }
}

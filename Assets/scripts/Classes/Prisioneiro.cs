using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisioneiro : UnitBehavior
{
    public Prisioneiro()
    {
        currentRank = 1;
        ClassGrowths = new() { 5, 0, 0, 0, 5, 0, 0, 5 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
        baseSkill = "Persistencia";
        skill1 = "Tecnica Improvisada";
        baseSoul = "Poder Oculto";
        soul1 = "Ataque Inspirador";
        UsableWeaponTypes = new() { Item.Weapontype.Sword };
        ClassGrowths = new() { 5, 0, 0, 0, 5, 0, 0, 5 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }

        
}
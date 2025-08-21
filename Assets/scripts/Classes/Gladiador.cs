using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiador : UnitBehavior
{
    public Gladiador()
    {
        currentRank = 1;
        ClassGrowths = new() { 10, 0, 0, 0, 10, 0, 0, 10 };
        classStats = new() { 10, 0, 0, 0, 2, 0, 0, 1 };
    }
    public void Awake()
    {
        baseSkill = "Persistencia";
        skill1 = "Tecnica Improvisada";
        baseSoul = "Poder Oculto";
        soul1 = "Ataque Inspirador";
        UsableWeaponTypes = new() { Item.Weapontype.Sword }; ClassGrowths = new() { 10, 0, 0, 0, 10, 0, 0, 10 };
        classStats = new() { 10, 0, 0, 0, 2, 0, 0, 1 };
    }

}

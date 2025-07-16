using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisioneiro : UnitBehavior
{
    public Prisioneiro()
    {
        currentRank = 1;
    }
    public void Awake()
    {
        baseSkill = "Persistencia";
        skill1 = "Tecnica Improvisada";
        baseSoul = "Poder Oculto";
        soul1 = "Ataque Inspirador";
        UsableWeaponTypes = new() { Item.Weapontype.Sword }; }
        
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espadachim : UnitBehavior
{
    public Espadachim()
    {
        currentRank = 1;
    }
    public void Awake()
    {
        baseSkill = "Dano Ascendente";
        skill1 = "Ataque Rápido";
        baseSoul = "Golpe Triplo";
         soul1 = "Golpe Focado";
       UsableWeaponTypes = new() { Item.Weapontype.Sword }; 
    }
}

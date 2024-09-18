using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Item")]
public class Item : ScriptableObject 
{
    public enum Type
    {
        weapon,
        accesory,
        key
    }
    public enum Weapontype
    {
        Sword,
        Axe,
        Lance,
        Bow,
        Tome,
        Receptacle,
        Accesory
    }
    public enum WeaponWeight
    {
        Light,
        Medium,
        Heavy

    }
    public UnitBehavior holder;
    public int id;
    public Sprite sprite;
    public Type type;
    public Weapontype weapontype;
    public WeaponWeight weight;
    public string ItemName;
    public string description;
    public int hit;
    public int crit;
    public int power;
    public int str;
    public int mag;
    public int dex;
    public int def;
    public int Mdef;
    public int luck;
    public int speed;
    public int price;
    public int damageType = 0;

    public virtual int ItemProc(int damage)
    {
        Debug.Log("ItemProc");
        return 0;
    }
}

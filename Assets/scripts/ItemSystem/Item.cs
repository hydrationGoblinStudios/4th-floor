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
        Firetome,
        Icetome,
        Thundertome,
        Plaguetome,
        Eviltome,
        Accesory

    }
    public enum WeaponWeight
    {
        Light,
        Medium,
        Heavy

    }

    public int id;
    public Sprite sprite;
    public Type type;
    public Weapontype weapontype;
    public WeaponWeight weight;
    public string ItemName;
    public string description;
    public int hit;
    public int crit;
    public int atk;
    public int dex;
    public int def;
    public int luck;
    public int speed;
    public int price;
}

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
    public int id;
    public Sprite sprite;
    public Type type;
    public string ItemName;
    public string description;
    public int hit;
    public int crit;
    public int atk;
    public int dex;
    public int def;
    public int luck;
    public int speed;
}

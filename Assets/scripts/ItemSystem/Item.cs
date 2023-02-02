using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum Type
    {
        weapon,
        accesory,
        key
    }
    public abstract class ItemObect: ScriptableObject
    {
        public int id;
        public Sprite sprite;
        public Type type;
        public string ItemName;
        public string description;
        public int hp;
        public int atk;
        public int dex;
        public int def;
        public int luck;
        public int speed;
    }
}

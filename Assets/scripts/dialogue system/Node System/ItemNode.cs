using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public Item item;
    public bool key;
    public bool flag;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (key)
        {
            Manager.KeyItems.Add(item);
        }
        else if (flag)
        {
            Manager.StoryFlags.Add(item);
        }
        else
        {
            Manager.Inventory.Add(item);
        }
        return "ItemNode/";
    }
}

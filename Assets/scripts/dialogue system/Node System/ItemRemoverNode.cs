using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemRemoverNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public Item item;
    public bool key;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (key)
        {
           Item toDelete = new();
           foreach(Item I in Manager.KeyItems)
            {
                toDelete = Manager.KeyItems.Where(obj => obj.name == item.name).SingleOrDefault();
            }
            Debug.Log(toDelete.name);
            Manager.KeyItems.Remove(toDelete);
        }
        else
        {
            Item toDelete = new();
            foreach (Item I in Manager.Inventory)
            {
                Manager.Inventory.Remove(Manager.Inventory.Where(obj => obj.name == item.name).SingleOrDefault());
            }
            Manager.Inventory.Remove(toDelete);
        }
        return "ItemNode/";
    }
}

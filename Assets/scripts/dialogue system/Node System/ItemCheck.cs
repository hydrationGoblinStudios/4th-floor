using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ItemCheck : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    [Output] public int exit2;
    public int id;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        int hasItem = 0;
        foreach(Item item in Manager.KeyItems)
        {
            if(item.id == id)
            {
                hasItem = 1;
            }
        }
        foreach (Item item in Manager.StoryFlags)
        {
            Debug.Log($"item:{item.name} id:{item.id}");
            if (item.id == id)
            {
                hasItem = 1;
            }
        }
        return "ItemCheck/" +hasItem;
    }
}

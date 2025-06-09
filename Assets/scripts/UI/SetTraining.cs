using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTraining : MonoBehaviour
{
    public InventoryManager inventory;

    public void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<InventoryManager>();
    }
    public void ChangeTraining(int id)
    {
        inventory.selectedUnit.fortalecerStat = id;
    }
}

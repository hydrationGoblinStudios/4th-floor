using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChangeCloser : MonoBehaviour
{
    public InventoryManager inventoryManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryManager.ToggleClassChange();
        }
    }
}

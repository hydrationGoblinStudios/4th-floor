using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscToClose : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Close();
        }
    }
    public void Close()
    {
        InventoryManager IM = FindObjectOfType<InventoryManager>(true);
        IM.Activatable = !IM.Activatable;
        MapToggle MT = FindObjectOfType<MapToggle>(true);
        MT.activatable = !MT.activatable;
        gameObject.SetActive(false);
    }
}

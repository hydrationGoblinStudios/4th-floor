using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public List<GameObject> invert;
    public bool activatable;
    public void Toggle()
    {
        if (activatable)
        {
            InventoryManager IM = FindObjectOfType<InventoryManager>(true); 
            IM.Activatable = !IM.Activatable;
            gameObject.SetActive(!gameObject.activeInHierarchy);
            invert.Clear();
            invert.Add(GameObject.FindGameObjectWithTag("Scene Interactables"));
            foreach (GameObject go in invert)
            {
                if(go != null)
                {
                 go.SetActive(!go.activeInHierarchy);
                }
            }        
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigToggle : MonoBehaviour
{
    public List<GameObject> invert;
    public bool activatable;
    public GameObject panel;
    public void Toggle()
    {
        if (activatable)
        {
            invert.Clear();
            invert.Add(FindObjectOfType<SceneInteractables>(true).gameObject);
            InventoryManager IM = FindObjectOfType<InventoryManager>(true);
            IM.Activatable = !IM.Activatable;
            MapToggle mt = FindObjectOfType<MapToggle>(true);
            mt.activatable = !mt.activatable;
            gameObject.SetActive(!gameObject.activeInHierarchy);

            foreach (GameObject go in invert)
            {
                if (go != null)
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
            if(activatable == false) { activatable = true; };
            Toggle();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
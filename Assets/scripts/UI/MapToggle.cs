using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public List<GameObject> invert;
    public bool activatable;
    public GameObject panel;
    public void Toggle()
    {
        GameObject GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        GameManager Manager = GameManagerOBJ.GetComponent<GameManager>();
        if(Manager.currentState == GameManager.UIState.Available)
        {

        if (activatable)
        {
        invert.Clear();
        invert.Add(FindObjectOfType<SceneInteractables>(true).gameObject);
            ConfigToggle CT = FindObjectOfType<ConfigToggle>(true);
            CT.activatable = !CT.activatable;
            InventoryManager IM = FindObjectOfType<InventoryManager>(true); 
            IM.Activatable = !IM.Activatable;
            gameObject.SetActive(!gameObject.activeInHierarchy);
            
            foreach (GameObject go in invert)
            {
                if(go != null)
                {
                 go.SetActive(!go.activeInHierarchy);
                }
            }
            foreach(Transform child in panel.transform)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                child.gameObject.GetComponent<SceneNameBasedHider>().Toggled(gm);
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

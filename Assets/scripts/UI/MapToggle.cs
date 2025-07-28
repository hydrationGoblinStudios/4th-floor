using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public List<GameObject> invert;
    public GameObject panel;
    public bool activated;
    public void Toggle()
    {
        activated = !activated;
        GameObject GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        GameManager Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (activated)
        {
            Manager.currentState = GameManager.UIState.Ocuppied;
        }
        else
        {
            Manager.currentState = GameManager.UIState.Available;
        }
        if(Manager.currentState == GameManager.UIState.Available || activated)
        {
            
            invert.Clear();
            invert.Add(FindObjectOfType<SceneInteractables>(true).gameObject);
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
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }
}

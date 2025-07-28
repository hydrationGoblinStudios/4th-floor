using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigToggle : MonoBehaviour
{
    public List<GameObject> invert;
    public GameObject panel;
    public void Toggle()
    {
      GameObject  GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
      GameManager  Manager = GameManagerOBJ.GetComponent<GameManager>();

        if (Manager.currentState == GameManager.UIState.Available)
        {
            invert.Clear();
            invert.Add(FindObjectOfType<SceneInteractables>(true).gameObject);
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
            Toggle();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
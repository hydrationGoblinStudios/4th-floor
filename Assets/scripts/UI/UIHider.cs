using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHIder : MonoBehaviour
{
    public GameObject ScreenManagerOBJ;
    public ScreenManager screenManager;

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ScreenManagerOBJ = GameObject.FindGameObjectWithTag("Screen Manager");
        if(ScreenManagerOBJ != null)
        {
        screenManager  = ScreenManagerOBJ.GetComponent<ScreenManager>();
        }
        if (screenManager != null && !screenManager.displayInventory)
        {
            transform.position = new Vector3(1000, transform.position.y, transform.position.z);
        }
        else if(screenManager != null && screenManager.displayInventory)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
}
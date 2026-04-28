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
        CheckScene();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckScene();
    }
    public void CheckScene()
    {
        ScreenManagerOBJ = GameObject.FindGameObjectWithTag("Screen Manager");
        if (ScreenManagerOBJ != null)
        {
            screenManager = ScreenManagerOBJ.GetComponent<ScreenManager>();
        }
        if (screenManager != null && !screenManager.displayInventory)
        {
            transform.localPosition = new Vector3(10000, transform.localPosition.y, transform.localPosition.z);
        }
        else if (screenManager != null && screenManager.displayInventory)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
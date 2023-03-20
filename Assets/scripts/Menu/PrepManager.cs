using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrepManager : MonoBehaviour
{
    public GameManager Manager;
    public GameObject GameManagerOBJ;
    public UnitBehavior UnitBehavior;
    public void Start()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
    }
    public void Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void MapToggle(GameObject mapObject)
    {
        mapObject.SetActive(!mapObject.activeInHierarchy);
    }
    public void ButtonToggle(Button button)
    {
        button.enabled = !button.enabled;

    }
}

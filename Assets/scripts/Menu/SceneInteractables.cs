using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInteractables : MonoBehaviour
{
    public string OriginalSceneName;
    public Scene m_Scene;
    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("scene name: " + m_Scene.name);
        Debug.Log("original scene name: " + OriginalSceneName);
        CheckIfSceneISOriginal(m_Scene);
    }
    public void CheckIfSceneISOriginal(Scene m_Scene)
    {
        m_Scene = SceneManager.GetActiveScene();
        Debug.Log("CheckIFSceneISORiginal: " + m_Scene.name);
        if (m_Scene.name == OriginalSceneName)
        {
            GameObject Go = GameObject.FindGameObjectWithTag("Interactables");
            this.transform.parent = Go.transform;
        }
        else
        {
            Debug.Log("destroy: " + m_Scene.name);
            Destroy(this.gameObject); 
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("rodou caralho");
        CheckIfSceneISOriginal(m_Scene);
    }

}

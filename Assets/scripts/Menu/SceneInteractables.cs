using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInteractables : MonoBehaviour
{
    [SerializeField]
    private string OriginalSceneName;
    [SerializeField]
    private Scene m_Scene;
    public GameObject InteractablesGO;
    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckIfSceneISOriginal();
        Debug.Log("scene name: " + m_Scene.name);
        Debug.Log("original scene name: " + OriginalSceneName);
    }
    private void CheckIfSceneISOriginal()
    {
        m_Scene = SceneManager.GetActiveScene();
        Debug.Log("CheckIFSceneISORiginal: " + m_Scene.name);
        if (m_Scene.name == OriginalSceneName)
        {
            InteractablesGO = GameObject.FindGameObjectWithTag("Interactables");
            if (InteractablesGO != null && this != null && this.gameObject != null)
            {
                this.transform.parent = InteractablesGO.transform;
            }
        }
        else
        {
            Debug.Log("destroy: " + OriginalSceneName + "Scene Interactables");
            if(this != null && this.gameObject != null)
            {
            Destroy(this.gameObject);
            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded:" + OriginalSceneName);
        CheckIfSceneISOriginal();
    }

}

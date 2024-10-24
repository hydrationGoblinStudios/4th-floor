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
    }
    private void CheckIfSceneISOriginal()
    {
        m_Scene = SceneManager.GetActiveScene();
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
            if(this != null && this.gameObject != null)
            {
            Destroy(this.gameObject);
            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckIfSceneISOriginal();
    }

}

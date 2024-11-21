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
    public GameManager manager;
    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckIfSceneISOriginal();
    }
    private void CheckIfSceneISOriginal()
    {
        Debug.Log("check");
        m_Scene = SceneManager.GetActiveScene();
        if (m_Scene.name == OriginalSceneName)
        {
            InteractablesGO = GameObject.FindGameObjectWithTag("Interactables");
            if (InteractablesGO != null && this != null && this.gameObject != null)
            {
                this.transform.parent = InteractablesGO.transform;
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name == OriginalSceneName)
                {
                manager.GameEventHandler();
                }
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

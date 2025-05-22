using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyTemp : MonoBehaviour
{
    private Scene m_Scene;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void CheckIfSceneIsbattle()
    {
        m_Scene = SceneManager.GetActiveScene();
        if (m_Scene.name == "Pre Battle" || m_Scene.name == "Battle")
        {
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
        CheckIfSceneIsbattle();
    }
}

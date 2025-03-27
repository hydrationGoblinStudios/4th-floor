using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongManager : MonoBehaviour
{
    public Scene m_scene;
    public AudioSource m_audioSource;
    public List<AudioClip> m_clip;
    public void ChecksScene()
    {
        m_scene = SceneManager.GetActiveScene();
        switch (m_scene.name)
        {
            case "Preparation1A": m_audioSource.clip = m_clip[0]; break;
            default:break;
        }
    }
}

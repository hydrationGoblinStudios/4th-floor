using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        AudioClip current = m_audioSource.clip;
        switch (m_scene.name)
        {
            case "Preparation1A": m_audioSource.clip = m_clip.Where(obj => obj.name == "floor1day").SingleOrDefault(); break;
            case "Abertura 2": m_audioSource.clip = m_clip.Where(obj => obj.name == "floor1day").SingleOrDefault(); break;
            case "Pre Battle": m_audioSource.clip = m_clip.Where(obj => obj.name == "youi!").SingleOrDefault(); break;
            case "Battle": m_audioSource.clip = m_clip.Where(obj => obj.name == "battle!").SingleOrDefault(); break;
            case "Cutscene": m_audioSource.clip = m_clip.Where(obj => obj.name == "senhor mistérios").SingleOrDefault(); break;

            default: break;
        }
        if (current != m_audioSource.clip)
        {
        m_audioSource.Play();
        }
    }
}

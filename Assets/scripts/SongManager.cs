using System;
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
    public float fadeOutTime = (float)25;
    public float fadeInTime = (float)0.2;
    private bool lowering = false;
    private float startVolume;
    private AudioClip current;
    public void ChecksScene()
    {
        startVolume = m_audioSource.volume;

        m_scene = SceneManager.GetActiveScene();
        current = m_audioSource.clip;
        lowering = true;
       switch (m_scene.name)
{
case "Preparation1A": m_audioSource.clip = m_clip.Where(obj => obj.name == "floor1day").SingleOrDefault(); break;
case "Abertura 2": m_audioSource.clip = m_clip.Where(obj => obj.name == "floor1day").SingleOrDefault(); break;
case "Pre Battle": m_audioSource.clip = m_clip.Where(obj => obj.name == "youi!").SingleOrDefault(); break;
case "Battle": m_audioSource.clip = m_clip.Where(obj => obj.name == "battle!").SingleOrDefault(); break;
default: break;
}
        if (current != m_audioSource.clip)
        {
            StartCoroutine(ChangeSong(m_scene.name));
        }
    }

    IEnumerator ChangeSong(string songName)
    {

        if (lowering)
        {
            m_audioSource.volume -= startVolume * Time.deltaTime * fadeOutTime;
        }
        else
        {
            m_audioSource.volume += startVolume * Time.deltaTime / fadeInTime;
        }
        yield return new WaitForSeconds((float)0.1);
        switch (m_scene.name)
        {
            case "Preparation1A": m_audioSource.clip = m_clip.Where(obj => obj.name == "floor1day").SingleOrDefault(); break;
            case "Abertura 2": m_audioSource.clip = m_clip.Where(obj => obj.name == "floor1day").SingleOrDefault(); break;
            case "Pre Battle": m_audioSource.clip = m_clip.Where(obj => obj.name == "youi!").SingleOrDefault(); break;
            case "Battle": m_audioSource.clip = m_clip.Where(obj => obj.name == "battle!").SingleOrDefault(); break;
            default: break;
        }
        if (m_audioSource.volume <= 0)
        {
            lowering = false;
            m_audioSource.Play();
        }
        if (!lowering && m_audioSource.volume == 1)
        {
            yield break;
        }
        else
        {
            StartCoroutine(ChangeSong(songName));
            yield break;
        }
    }
}

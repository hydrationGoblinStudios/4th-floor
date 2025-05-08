using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public AudioClip song;


    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Manager.songManager.m_audioSource.clip = song;
        Manager.songManager.m_audioSource.Play();
        return "SongNode/";
    }
}
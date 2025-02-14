using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventCaller : MonoBehaviour
{
    public GameManager manager; 

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
        manager.GameEventHandler();
    }
}


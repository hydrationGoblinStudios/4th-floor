using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GMButtonAssigner : MonoBehaviour
{
    public GameManager gameManager;
    public Button button;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }
    public void Sleep()
    {
        gameManager.Sleep();
    }
}

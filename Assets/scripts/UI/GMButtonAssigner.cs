using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GMButtonAssigner : MonoBehaviour
{
    public GameManager gameManager;
    public Button button;

    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
        button.onClick.AddListener(delegate { gameManager.Sleep(); });
    }
    public void Sleep()
    {
        //gameManager.Sleep();
    }

}

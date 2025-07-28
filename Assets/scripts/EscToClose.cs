using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscToClose : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Close();
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}

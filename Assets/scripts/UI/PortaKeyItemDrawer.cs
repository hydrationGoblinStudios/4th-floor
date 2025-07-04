using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortaKeyItemDrawer : MonoBehaviour
{  
    public Button PortaButton;
    public KeyItemDrawer KID;
    void Start()
    {
        KID = FindAnyObjectByType<KeyItemDrawer>();
    }
    void Update()
    {
        if (KID.activated)
        {
            PortaButton.enabled = false;
        }
        else
        {
            PortaButton.enabled = true;
        }
    }
}

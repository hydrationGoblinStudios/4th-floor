using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLogScroller : MonoBehaviour
{
    public GameObject dialogueHistoryTarget;


    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            dialogueHistoryTarget.transform.position = dialogueHistoryTarget.transform.position + new Vector3(0, 0.4f, 0);
            Debug.Log("mouse wheel up");
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            dialogueHistoryTarget.transform.position = dialogueHistoryTarget.transform.position + new Vector3(0, -0.4f, 0);

            Debug.Log("mouse wheel down");
        }
    }
}

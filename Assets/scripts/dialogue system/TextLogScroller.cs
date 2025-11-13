using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextLogScroller : MonoBehaviour
{
    public GameObject background;
    public GameObject dialogueHistoryTarget;
    public RectTransform textBox;

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            background.transform.position = background.transform.position + new Vector3(0, 0.4f, 0);
            if (dialogueHistoryTarget.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.Overlaps(textBox.rect))
            {
                Debug.Log("overlap");
                dialogueHistoryTarget.transform.position = background.transform.position + new Vector3(0, -0.4f, 0);
            }   
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            background.transform.position = background.transform.position + new Vector3(0, -0.4f, 0);
            if (dialogueHistoryTarget.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.Overlaps(textBox.rect))
            {
                Debug.Log("overlap");
                dialogueHistoryTarget.transform.position = background.transform.position + new Vector3(0, 0.4f, 0);
            }
        }
    }
}

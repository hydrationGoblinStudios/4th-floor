using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleStatsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int character;
    public BattleManager bm;
    public bool hovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        bm.hovering = true;
        hovering = true;
        bm.hoverStatUpdate(character);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hovering)
        {
            bm.hovering = false;
            hovering = false;
        }
    }
}

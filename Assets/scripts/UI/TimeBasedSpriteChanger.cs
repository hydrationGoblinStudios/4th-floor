using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedSpriteChanger : MonoBehaviour
{
    public SpriteRenderer TurnOnIfNight;
    public void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (!gm.storyBattle)
        {
            TurnOnIfNight.color = new(255, 255, 255, 255);
        }
    }
}

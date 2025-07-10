using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedSpriteChanger : MonoBehaviour
{
    public SpriteRenderer TurnOnIfNight;
    public List<Sprite> Sprites;
    public void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (!gm.storyBattle)
        {
            TurnOnIfNight.sprite = Sprites[1];
        }
    }
}
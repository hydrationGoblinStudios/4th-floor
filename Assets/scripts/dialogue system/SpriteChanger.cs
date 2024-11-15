using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public void ChangeSprite(int sprite)
    {
        spriteRenderer.sprite = sprites[sprite];
    }
}

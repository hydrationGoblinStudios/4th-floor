using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemStateManager : MonoBehaviour
{
    public DialogueGraph target;
    public Sprite sprite;
    public List<Item> items;
    public List<GameObject> interactables;
    public List<SpriteRenderer> sprites;
    public GameManager manager;

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
        foreach (Item item in manager.KeyItems)
        {
            if (items[0].name == item.name)
            {
                sprites[0].sprite = sprite;
                interactables[0].GetComponent<ButtonAssigner>().graph = target;
                interactables[0].GetComponent<ButtonAssigner>().AddListener();
            }
        }
        foreach (Item item in manager.StoryFlags)
        {
                if (items[0].name == item.name)
            {
            sprites[0].sprite = sprite;
            interactables[0].GetComponent<ButtonAssigner>().graph = target;
            interactables[0].GetComponent<ButtonAssigner>().AddListener();
            }   
            
        }
    }
}

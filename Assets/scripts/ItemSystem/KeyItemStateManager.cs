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
            int c = 0;

            if (items[0].name == item.name)
            {
                Debug.Log("keyitem");
                sprites[c].sprite = sprite;
                interactables[0].GetComponent<ButtonAssigner>().graph = target;
                interactables[0].GetComponent<ButtonAssigner>().AddListener();
                c++;
            }
        }
        foreach (Item item in manager.StoryFlags)
        {
            int c = 0;
            Debug.Log(items[0].name);
            Debug.Log(item.name);
                if (items[0].name == item.name)
            {
            Debug.Log("storyFlag");
           sprites[c].sprite = sprite;
            interactables[0].GetComponent<ButtonAssigner>().graph = target;
            interactables[0].GetComponent<ButtonAssigner>().AddListener();
            }   
                c++;
            
        }
    }
}

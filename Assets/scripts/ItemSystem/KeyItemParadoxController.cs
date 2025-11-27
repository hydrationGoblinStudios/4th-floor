using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemParadoxController : MonoBehaviour
{
    public DialogueGraph vazio;
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
            foreach(GameObject i in interactables)
            {

                try
                {
                if(i.name == item.name + " Interactable")
                {
                    sprites[c].sprite = null;
                    i.GetComponent<ButtonAssigner>().graph = vazio;
                    i.GetComponent<ButtonAssigner>().AddListener();
                }
                }
                catch
                {
                }
                    c++;
            }
        }
        foreach (Item item in manager.StoryFlags)
        {
            int c = 0;
            foreach (GameObject i in interactables)
            {
                if (i.name == item.name + " Interactable")
                {
                    sprites[c].sprite = null;
                    i.GetComponent<ButtonAssigner>().graph = vazio;
                    i.GetComponent<ButtonAssigner>().AddListener();
                }
                c++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hanoi : MonoBehaviour
{

    public List<int> pillar1 = new() {1,2,3,4};
    public List<int> pillar2 = new();
    public List<int> pillar3 = new();
    public List<List<int>> pillars = new();
    public bool holding;
    public int held;
    public DialogueGraph graph;
    public List<SpriteRenderer> sacks1;
    public List<SpriteRenderer> sacks2;
    public List<SpriteRenderer> sacks3;
    public List<SpriteRenderer> heldSprites;

    public void Start()
    {
        pillars.Add(pillar1);
        pillars.Add(pillar2);
        pillars.Add(pillar3);
        CheckSprites();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneMaracutaia sm = FindObjectOfType<SceneMaracutaia>(true);
            sm.LoadScene("Dispensa");
        }
    }
    public void Button(int i)
    {
        if (holding && pillars[i][0] > held)
        {
           pillars[i].Insert(0, held);
           holding = false;
            held = 0;

            Debug.Log(pillar3.Count);
            
            if (pillar3.Count == 5)
            {
                SceneMaracutaia sm = FindObjectOfType<SceneMaracutaia>(true);
                sm.LoadSceneNDialogue("Dispensa", graph);
            }
            CheckSprites();
        }
        else if(!holding && pillars[i][0] != 5)
        {

            if (pillars[i].Count >= 1)
            {
                held = pillars[i][0];
                holding = true;
                pillars[i].Remove(held);
            }
            CheckSprites();
        }
    }
    public void CheckSprites()
    {
        foreach (SpriteRenderer sr in sacks1)
        {
            sr.enabled = false;
        }
        foreach (int n in pillar1)
        {
            sacks1.Where(obj => obj.gameObject.name == n.ToString()).SingleOrDefault().enabled = true;
        }
        foreach (SpriteRenderer sr in sacks2)
        {
            sr.enabled = false;
        }
        foreach (int n in pillar2)
        {
            sacks2.Where(obj => obj.gameObject.name == n.ToString()).SingleOrDefault().enabled = true;
        }
        foreach (SpriteRenderer sr in sacks3)
        {
            sr.enabled = false;
        }
        foreach (int n in pillar3)
        {
            sacks3.Where(obj => obj.gameObject.name == n.ToString()).SingleOrDefault().enabled = true;
        }
        foreach (SpriteRenderer sr in heldSprites)
        {
            sr.enabled = false;
        }       
        if(held >0 && held <5)
        {
            heldSprites.Where(obj => obj.gameObject.name == held.ToString()).SingleOrDefault().enabled = true;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoisaQueVaiNoGelo : MonoBehaviour
{
    public float speed;
    public int direction;
    public bool actionable;
    public float timer;
    public GameObject canvas;
    public List<GameObject> grid;
    public List<int> blocks;
    public List<int> corners;
    public int position;
    public DialogueGraph graph;

    private void Start()
    {
        foreach (Transform go in canvas.transform)
        {
            grid.Add(go.gameObject);
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
        List<int> list = new List<int>() { 0, 12, 24, 36, 48, 60, 72, 84, 96, 108, 120, 132, 144, 156, 168 , 180 };
        if (Input.GetKeyDown(KeyCode.UpArrow) && actionable == true && !list.Contains(position) && !blocks.Contains(position -1))
        {
            actionable = false;
            direction = 1;
        }
        list = new List<int>() { 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179 };
        if (Input.GetKeyDown(KeyCode.RightArrow) && actionable == true && !list.Contains(position) && !blocks.Contains(position + 12))
        {
            actionable = false;
            direction = 2;
        }
        list = new List<int>() { 11, 23, 35, 47, 59, 71, 83, 95, 107, 119, 131, 143, 155, 167, 179 };
        if (Input.GetKeyDown(KeyCode.DownArrow) && actionable == true && !list.Contains(position)&& !blocks.Contains(position + 1))
        {
            actionable = false;
            direction = 3;
        }
        list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        if (Input.GetKeyDown(KeyCode.LeftArrow) && actionable == true && !list.Contains(position) && !blocks.Contains(position - 12))
        {
            actionable = false;
            direction = 4;
        }
        List<int> blocksTemp = new List<int>();
        switch (direction)
        {
            case 1:
                list = new List<int>() { 0, 12, 24, 36, 48, 60 , 72 , 84 , 96, 108, 120, 132, 144, 156 , 168,180}; position -= 1;
                blocksTemp = new List<int>();
                foreach (int n in blocks) { blocksTemp.Add(n + 1); }
                gameObject.transform.position = grid[position].transform.position; 
                while (!list.Contains(position) && !blocksTemp.Contains(position)) { position -= 1; gameObject.transform.position = grid[position].transform.position;} 
                direction = 0; actionable = true; break;
            case 2:
                list = new List<int>() { 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179 }; position += 12;
                blocksTemp = new List<int>();
                foreach (int n in blocks) { blocksTemp.Add(n - 12); }
                gameObject.transform.position = grid[position].transform.position;
                while (!list.Contains(position) && !blocksTemp.Contains(position)) { position += 12; gameObject.transform.position = grid[position].transform.position;}
                direction = 0; actionable = true;  break;
            case 3:
                list = new List<int>() { 11, 23, 35, 47, 59, 71, 83, 95, 107, 119, 131, 143, 155, 167, 179 }; position += 1; gameObject.transform.position = grid[position].transform.position;
                blocksTemp = new List<int>();
                foreach (int n in blocks) { blocksTemp.Add(n - 1); }
                while (!list.Contains(position) && !blocksTemp.Contains(position)) { position += 1; gameObject.transform.position = grid[position].transform.position; }
                direction = 0; actionable = true; break;
            case 4:
                list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                blocksTemp = new List<int>();
                foreach (int n in blocks) { blocksTemp.Add(n + 12); }
                position -= 12; gameObject.transform.position = grid[position].transform.position; 
                while (!list.Contains(position) && !blocksTemp.Contains(position)) { position -= 12; gameObject.transform.position = grid[position].transform.position; }
                direction = 0; actionable = true; break;
            default: break;
        }
        if (corners.Contains(position))
        {
            position = 76; gameObject.transform.position = grid[position].transform.position;
        }
        if(position == 95)
        {
            SceneMaracutaia sm = FindObjectOfType<SceneMaracutaia>(true);
            sm.LoadSceneNDialogue("Quarto Dos Guardas", graph);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneMaracutaia sm = FindObjectOfType<SceneMaracutaia>(true);
            sm.LoadScene("Quarto Dos Guardas");
        }
    }
}
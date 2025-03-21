using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoisaQueVaiNoGelo : MonoBehaviour
{
    public float speed;
    public int direction;
    public bool actionable;
    public float timer;
    public GameObject canvas;
    public List<GameObject> grid;
    public int position;

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
        List<int> list = new List<int>(){0,3,6,9,12};
        if (Input.GetKeyDown(KeyCode.UpArrow) && actionable == true && !list.Contains(position))
        {
            actionable = false;
            direction = 1;
        }
        list = new List<int>() {12,13,14};
        if (Input.GetKeyDown(KeyCode.RightArrow) && actionable == true && !list.Contains(position))
        {
            actionable = false;
            direction = 2;
        }
        list = new List<int>() { 2, 5, 8, 11, 14 };
        if (Input.GetKeyDown(KeyCode.DownArrow) && actionable == true && !list.Contains(position))
        {
            actionable = false;
            direction = 3;
        }
        list = new List<int>() { 0,1,2 };
        if (Input.GetKeyDown(KeyCode.LeftArrow) && actionable == true && !list.Contains(position))
        {
            actionable = false;
            direction = 4;
        }
        switch (direction)
        {
            case 1:
                list = new List<int>() { 0, 3, 6, 9, 12 }; position -= 1; gameObject.transform.position = grid[position].transform.position; 
                while (!list.Contains(position)) { position -= 1; gameObject.transform.position = grid[position].transform.position; } 
                direction = 0; actionable = true; break;
            case 2:
                list = new List<int>() { 12, 13, 14 }; position += 3; gameObject.transform.position = grid[position].transform.position;
                while (!list.Contains(position)) { position += 3; gameObject.transform.position = grid[position].transform.position; }
                direction = 0; actionable = true;  break;
            case 3:
                list = new List<int>() { 2, 5, 8, 11, 14 }; position += 1; gameObject.transform.position = grid[position].transform.position; 
                while (!list.Contains(position)) { position += 1; gameObject.transform.position = grid[position].transform.position; }
                direction = 0; actionable = true; break;
            case 4:
                list = new List<int>() { 0, 1, 2 };
                position -= 3; gameObject.transform.position = grid[position].transform.position; 
                while (!list.Contains(position)) { position -= 3; gameObject.transform.position = grid[position].transform.position; }
                direction = 0; actionable = true; break;
            default: break;
        }
    }
}
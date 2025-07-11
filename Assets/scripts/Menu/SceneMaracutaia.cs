using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaracutaia : MonoBehaviour
{
    public GameManager gameManager;
    public DialogueGraph[] barrarEntrada;

    public void PlayGame()
    {
        SceneManager.LoadScene("Abertura");
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadGame()
    {
        string FileName = "/AlvorecerLunar.json";

        if (Application.isEditor)
        {
            FileName = "/AlvorecerLunarEditor.json";
        }
        Debug.Log(Application.persistentDataPath + FileName);
        if (System.IO.File.Exists(Application.persistentDataPath + FileName))
        {
            Debug.Log("tem file");
            SceneManager.LoadScene("LoadSave");
        }
    }
    public void LoadSceneNDialogue(string scene, DialogueGraph graph)
    {
        SceneManager.LoadScene(scene);
        NodeParser nodeParser = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<NodeParser>();
        nodeParser.StartDialogue(graph);
    }
    public void LoadCombat(string scene)
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        if (gameManager.day == 4 && gameManager.team.Count <=2)
        {
            NodeParser dm = FindObjectOfType<NodeParser>(true);
            dm.StartDialogue(barrarEntrada[1]);
        }
        else if(gameManager != null && gameManager.team[0].GetComponent<UnitBehavior>().Weapon.ItemName != "" && gameManager.storyBattle == true)
        {
            SceneManager.LoadScene(scene);
        }
        else if (!gameManager.storyBattle)
        {
            NodeParser dm = FindObjectOfType<NodeParser>(true);
            dm.StartDialogue(barrarEntrada[2]);
        }
        else
        {
            NodeParser dm = FindObjectOfType<NodeParser>(true);
            dm.StartDialogue(barrarEntrada[0]);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PrepScreen()
    {
        SceneManager.LoadScene("Preparation");
    }
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }
}

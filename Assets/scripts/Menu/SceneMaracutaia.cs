using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaracutaia : MonoBehaviour
{
    public GameManager gameManager;

    public void PlayGame()
    {
        SceneManager.LoadScene("Abertura");
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
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
        if (gameManager != null && gameManager.team[0].GetComponent<UnitBehavior>().Weapon.ItemName != "")
        {
            SceneManager.LoadScene(scene);
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

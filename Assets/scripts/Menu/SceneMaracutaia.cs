using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaracutaia : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Preparation");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

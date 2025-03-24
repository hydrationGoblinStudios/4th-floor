using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneNameBasedHider : MonoBehaviour
{
    public string OriginalSceneName;
    public void Toggled()
    {
        if(SceneManager.GetActiveScene().name == OriginalSceneName)
        {
           transform.position = new Vector3(10000, transform.position.y, transform.position.z);
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = new Vector3(80, transform.position.y, transform.position.z);
            gameObject.SetActive(true);

        }
    }

}

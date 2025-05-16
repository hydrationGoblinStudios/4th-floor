using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneNameBasedHider : MonoBehaviour
{
    public string OriginalSceneName;
    public void Toggled(GameManager gm)
    {
            if(SceneManager.GetActiveScene().name == OriginalSceneName || !gm.unlockedMaps.Contains(OriginalSceneName))
                {
                   transform.position = new Vector3(10000, transform.position.y, transform.position.z);
                    gameObject.SetActive(false);
                }
            else if(gm.unlockedMaps.Contains(OriginalSceneName))
                {
                    transform.position = new Vector3(80, transform.position.y, transform.position.z);
                    gameObject.SetActive(true);
                }       
    }
}

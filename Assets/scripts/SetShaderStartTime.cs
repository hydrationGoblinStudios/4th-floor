using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShaderStartTime : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;
    void Start()
    {
        renderer.material.SetFloat("_StartTime", Time.time);
    }
}

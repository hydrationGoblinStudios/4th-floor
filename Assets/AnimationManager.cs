using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public Animation playerAnimation;
    public Animation enemyAnimation;
    
    void Start()
    {
        animator  = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}

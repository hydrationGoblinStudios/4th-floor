using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public AnimatorController[] Animations;
    public Animator Panimator;
    public Animator Eanimator;
    public GameObject PanimatorOBJ;
    public GameObject EanimatorOBJ;
    public BattleManager battleManager;

    public bool wait = true;
    void Start()
    {
        PanimatorOBJ = GameObject.FindGameObjectWithTag("Psprite");
        EanimatorOBJ = GameObject.FindGameObjectWithTag("Esprite");
        Panimator = PanimatorOBJ.GetComponent<Animator>();
        Eanimator = EanimatorOBJ.GetComponent<Animator>();
        Panimator.runtimeAnimatorController = Animations[0];
    }
    void Update()
    {
       if(battleManager.state == BattleManager.BattleState.PlayerTurn & wait)
        {
            Panimator.SetTrigger("Pattack");
            wait = false; 
        }
        if (battleManager.state == BattleManager.BattleState.Wait)
        {
            wait = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public RuntimeAnimatorController[] Animations;
    public Animator[] animators;
    public BattleManager battleManager;

    public bool wait = true;
    void Start()
    {
        SetClass(animators[0], battleManager.playerTeam[0]);
        SetClass(animators[1], battleManager.playerTeam[1]);
        SetClass(animators[2], battleManager.playerTeam[2]);
        SetClass(animators[3], battleManager.enemyTeam[0]);
        SetClass(animators[4], battleManager.enemyTeam[1]);
        SetClass(animators[5], battleManager.enemyTeam[2]);
    }
    public void SetClass(Animator animator, UnitBehavior ub)
    {
        animator.runtimeAnimatorController = ub.classId switch
        {
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            107 => Animations[6],
            _ => Animations[0],
        };
    }
}

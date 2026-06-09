using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationCallBack : MonoBehaviour
{
    public void CallBack()
    {
        Debug.Log(gameObject.name + " is calling attack trigger.");
        BattleManager.Instance.AnimationTrigger();
    }
}

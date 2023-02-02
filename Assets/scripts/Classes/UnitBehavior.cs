using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitBehavior : MonoBehaviour
{
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    public bool enemy;
    [Header("parameters")]
    public string UnitName;
    public int maxhp;
    public int hp;
    public int atk;
    public int dex;
    public int def;
    public int luck;
    public float speed;
    public TextMeshPro battleText;
    public GameObject battleTextObj;
    void Start()
    {   //lembrar de manter o objeto de player acima do objeto do inimigo
        UnitBehavior playerBehavior = GetComponent<UnitBehavior>();
        UnitBehavior enemyBehavior = GetComponent<UnitBehavior>();
        battleTextObj = GameObject.FindGameObjectWithTag("Battle Text");
        battleText = battleTextObj.GetComponent<TextMeshPro>();
        if (enemy)
        {
            UnitName = "EvilBors";
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }
    }
    public virtual int Soul(int damage)
    {
        Debug.Log("Not Valid Class");
        return 0;
    }
    public virtual int Proc(int damage)
    {
        Debug.Log("skill proc");
        return 0;
    }
}

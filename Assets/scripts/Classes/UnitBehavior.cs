using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitBehavior : MonoBehaviour
{
    public Item Weapon;
    public Item Accesory;
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    public bool enemy;
    [Header("parameters")]
    public string UnitName;
    public int hit;
    public int crit;
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
         playerBehavior = GetComponent<UnitBehavior>();
         enemyBehavior = GetComponent<UnitBehavior>();
        battleTextObj = GameObject.FindGameObjectWithTag("Battle Text");
        battleText = battleTextObj.GetComponent<TextMeshPro>();
        if (enemy)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }
        if(Weapon != null)
        {
            hit += Weapon.hit;
            crit += Weapon.crit;
            hp += Weapon.hp;
            atk += Weapon.atk;
            dex += Weapon.dex;
            def += Weapon.def;
            luck += Weapon.luck;
            speed += Weapon.speed;
        }
        if (Accesory != null)
        {
            hit += Weapon.hit;
            hp += Weapon.hp;
            atk += Weapon.atk;
            dex += Weapon.dex;
            def += Weapon.def;
            luck += Weapon.luck;
            speed += Weapon.speed;
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

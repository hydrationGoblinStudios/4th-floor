using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitBehavior : MonoBehaviour
{
    public int id;
    public Item Weapon;
    public Item Accesory;
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    public BattleManager battleManager;
    public bool enemy;
    public GameObject battleManagerOBJ;
    public string UnitName;
    public int level;
    public int exp;
    public int hit;
    public int avoid;
    public int crit;
    public int maxhp;
    public int hp;
    public int str;
    public int mag;
    public int dex;
    public int def;
    public int mdef;
    public int luck;
    public float speed;
    public bool Pendure;
    public bool Eendure;
    public TextMeshPro battleText;
    public GameObject battleTextObj;
    public List<int> growths;
    public string description = "classe não valida";
    void Start()
    {   //lembrar de manter o objeto de player acima do objeto do inimigo
         playerBehavior = GetComponent<UnitBehavior>();
         enemyBehavior = GetComponent<UnitBehavior>();
        battleTextObj = GameObject.FindGameObjectWithTag("Battle Text");
        battleText = battleTextObj.GetComponent<TextMeshPro>();
        battleManagerOBJ = GameObject.FindGameObjectWithTag("Battle Manager");
        Pendure = false;
        Eendure = false;
        if (battleManagerOBJ != null)
        {
            battleManager = battleManagerOBJ.GetComponent<BattleManager>();
        }
        if (enemy)
        {
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

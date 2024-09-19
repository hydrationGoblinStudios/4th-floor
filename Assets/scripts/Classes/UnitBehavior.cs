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
    [Header("Parameters")]
    public string UnitName;
    public int level;
    public int exp;
    public int hit;
    public int avoid;
    public int crit;
    [Header("Stats")]
    public int maxhp;
    public int hp;
    public int power;
    public int str;
    public int mag;
    public int dex;
    public int def;
    public int mdef;
    public int[] defenses;
    public int luck;
    public float speed;
    public int soul;
    public int maxsoul;
    public int soulgain;
    public bool Pendure;
    public bool Eendure;
    public TextMeshPro battleText;
    public GameObject battleTextObj;
    public List<int> growths;
    public string description = "classe n?o valida";
    void Start()
    {   //lembrar de manter o objeto de player acima do objeto do inimigo
         playerBehavior = GetComponent<UnitBehavior>();
        enemyBehavior = GetComponent<UnitBehavior>();
        Weapon.holder = this;
        battleTextObj = GameObject.FindGameObjectWithTag("Battle Text");
        battleText = battleTextObj.GetComponent<TextMeshPro>();
        battleManagerOBJ = GameObject.FindGameObjectWithTag("Battle Manager");
        Pendure = false;
        Eendure = false;
        defenses = new int[2]; defenses[0] = def;defenses[1] = mdef;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : UnitBehavior
{
    public string descriptionA = "O Soldado ataca reduzindo a barra de ação do inimigo";
    public string descriptionP = "Chance baseada em destreza de causar mais dano quanto mais vida o Soldado tiver";
    public override int Soul(int damage)
    {
        if(enemy)
        {
            battleManager.battleText.text = "Golpe Atordoante!";
            battleManager.PlayerBar -= 50;
        }
        else
        {
            battleManager.battleText.text = "Golpe Atordoante!";
            battleManager.EnemyBar -= 50;
        }
        return damage/4 +1;
    }
    public override int Proc(int damage)
    {
        if(Random.Range(0,101) < dex)
        {
            battleManager.battleText.text = "Perfurar ativou!";
            return hp / 4 + 1;
        }
        return 0;
    }
}
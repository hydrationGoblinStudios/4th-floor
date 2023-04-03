using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : UnitBehavior
{
    public string descriptionA = "O Barbaro ataca mais rapido pelo resto da batalha";
    public string descriptionP = "Se o barbaro for mais forte que o oponente ele ira se curar em parte do dano";
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
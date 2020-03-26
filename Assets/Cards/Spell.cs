using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Card/Spell", order = 2)]
public class Spell : ScriptableObject
{
    public bool attackArmor;
    public int exposedTurns;
    public int damageOverTimeTurns;
    public int damageOverTimeVal;

    public void UseSpell()
    {
        if (exposedTurns > 0)
        {
            Enemy.Instance.UpdateExposed(exposedTurns);
        }
       
        if (attackArmor)
        {
            Player.Instance.InitAttackArmor(damageOverTimeTurns, damageOverTimeVal);
        }

        UseCustomSpell();
    }

    public virtual void UseCustomSpell()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CreateAssetMenu(fileName = "Spell", menuName = "Card/Spell", order = 2)]
public class Spell : ScriptableObject
{
    public bool attackArmor;
    public int exposedTurns;
    public int damageOverTimeTurns;
    public int damageOverTimeVal;

    public int numTurns;
    //ComplexSpell
    //public int numTurns;
    public int effectDamage;

    //protected int effectTurns;

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

    public virtual void ExecuteEffect()
    {
        if(numTurns > 0)
        {
            
        }
        else
        {
            //SpellManager.Instance.SpellObjects.Remove(this); handled by spellmanager

        }
    }

    public void AddToSpellManager() //just call line belopw maybe ?
    {
        SpellManager.Instance.AddSpellEffect(this, numTurns);
    }


}

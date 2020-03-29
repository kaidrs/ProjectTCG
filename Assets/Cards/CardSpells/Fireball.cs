using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball_SO", menuName = "Card/CustomSpell/Fireball", order = 3)]
public class Fireball : Spell
{

    public override void UseCustomSpell()
    {
        AddToSpellManager();
        Debug.Log("Casted fireball");
        
    }

    public override void ExecuteEffect()
    {
        base.ExecuteEffect();
        Debug.Log("Executed effect of " + name);
    }

}

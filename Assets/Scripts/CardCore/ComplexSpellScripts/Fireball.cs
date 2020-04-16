using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball_SO", menuName = "Card/CustomSpell/Fireball", order = 3)]
public class Fireball : Spell
{
    public override void ExecuteEffect()
    {
        base.ExecuteEffect();
        Enemy.Instance.TakeDamage(effectDamage);
        Debug.Log("Executed effect of " + name);
    }

    public override void UseCustomSpell()
    {
        
    }


}

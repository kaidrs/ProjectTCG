using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball_SO", menuName = "Card/CustomSpell/Fireball", order = 3)]
public class Fireball : Spell
{
    public override void UseCustomSpell()
    {
        Debug.Log("Used fireball");
    }

}

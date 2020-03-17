using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string desc;
    public int mana;
    public int attack;
    public Sprite art;

    public bool isSpell;
    public bool attackArmor;
    public int armorValue;
    public int exposedTurns;
    public int damageOverTimeTurns;
    public int damageOverTimeVal;
}

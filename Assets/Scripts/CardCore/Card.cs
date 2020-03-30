using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Regular", order = 1)]
public class Card : ScriptableObject
{
    public new string name;
    public string desc;
    public int mana;
    public int attack;
    public int armorValue;
    public Sprite art;
    public Spell spellObject;

    public void UseCard()
    {
        if (attack > 0)
        {
            Debug.Log("using " + name);
            Enemy.Instance.TakeDamage(attack);
        }
        if (armorValue > 0)
        {
            if (Player.Instance.AttackArmor)
            {
                Enemy.Instance.TakeDamage(Player.Instance.AaDmgVal);
            }
            Player.Instance.UpdateArmor(armorValue);
        }
        if (spellObject != null)
        {
            spellObject.UseSpell();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "Necromancy_SO", menuName = "Card/CustomSpell/Necromancy", order = 3)]
public class Necromancy : Spell
{

    public override void UseCustomSpell()
    {
        UIManager.Instance.DiscardPanel.SetActive(true);

    }
}

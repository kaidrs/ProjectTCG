using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CustomExtension;
using System;

public class SpellManager : MonoBehaviour
{
    private static SpellManager instance = null;
    public static SpellManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SpellManager>();
            }
            return instance;
        }
    }

    //List<Tuple<Spell, int>> spellObjects = new List<Tuple<Spell, int>>(); // possibility of dict instead due to search amts
    [SerializeField] List<Spell> spellObjects = new List<Spell>();
    [SerializeField] private List<int> spellNumTurns = new List<int>();
    int indexToRemove = -1;

    public void FlushSpellEffects()
    {
        spellObjects.Clear();
        spellNumTurns.Clear();
    }

    public void AddSpellEffect(Spell sp, int numTurns)
    {
        spellObjects.Add(sp);
        spellNumTurns.Add(numTurns);
    }

    public void ExecuteEffects()
    {
        int temp = 0;
        
        if (spellObjects.Count > 0)
        {
            foreach (var (item, index) in spellObjects.WithIndex())
            {
                temp = spellNumTurns.ElementAt(index);
                if(temp > 0)
                {
                    temp--;
                    spellNumTurns.RemoveAt(index);
                    spellNumTurns.Insert(index, temp);
                    spellObjects.ElementAt(index).ExecuteEffect();
                    Debug.Log($"now {temp} turns left for {item.name} effect");
                }
                else
                {
                    indexToRemove = index;
                    break;
                }
            }

            if(indexToRemove != -1) {
                spellObjects.RemoveAt(indexToRemove);
                spellNumTurns.RemoveAt(indexToRemove);
                indexToRemove = -1; 
            }

        }
    }

}

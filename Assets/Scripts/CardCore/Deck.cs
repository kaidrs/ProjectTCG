using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Deck", menuName = "Deck", order = 1)]
public class Deck : ScriptableObject
{
    public List<Card> deckList;
}

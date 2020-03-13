using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static CardManager instance = null;
    public static CardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CardManager>();
            }
            return instance;
        }
    }

    public List<Card> DeckList { get => deckList; set => deckList = value; }

    Enemy target;
    Player player;

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardObject;

    [SerializeField] List<Card> deckList = new List<Card>();
    [SerializeField] List<GameObject> cardList = new List<GameObject>();
    Stack<Card> deck;

    bool flushing;
    int cardIndex;
    const int handSize = 6;
   
    private void Start()
    {
        player = GetComponent<Player>();
        UpdateTarget();
        ShuffleDeck(deckList);
        deck = new Stack<Card>(deckList);
        DrawCards();
    }

    void UpdateTarget()
    {
        target = FindObjectOfType<Enemy>();
    }

    public static List<Card> ShuffleDeck(List<Card> aList)
    {
        //Fisher Yates Shuffle implementation
        System.Random random = new System.Random();

        Card myCard;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(random.NextDouble() * (n - i));
            myCard = aList[r];
            aList[r] = aList[i];
            aList[i] = myCard;
        }

        return aList;
    }

    //When drawing complete hand, called on start of fight and end turns
    void DrawCards()
    {
        flushing = false;
        if (deck.Count >= handSize)
        {
            for (int i = 0; i < handSize; i++)
            {
                cardObject.GetComponent<CardDisplay>().Card = deck.Peek();

                cardList.Add(Instantiate(cardObject, handParent.transform));
                deck.Pop();
            }
        }

        else if (deck.Count < handSize && deck.Count > 0)
        {
            for (int i = 0; i < deck.Count+1; i++)
            {
                cardObject.GetComponent<CardDisplay>().Card = deck.Peek();

                cardList.Add(Instantiate(cardObject, handParent.transform));
                deck.Pop();
            }

        }

        else
        {
            Debug.Log("Deck empty - Cannot draw!");
        }

        Debug.Log("Current cards left in deck : " + deck.Count);
    }

    void DrawCards(int num)
    {
        if(deck.Count >= num)
        {
            for (int i = 0; i < num; i++)
            {
                cardObject.GetComponent<CardDisplay>().Card = deck.Peek();
                Instantiate(cardObject, handParent.transform);
                deck.Pop();
            }
        }

    }

    //Called by endturn
    public void FlushCards()
    {
        if (!flushing)
        {
            flushing = true;
            foreach (var obj in cardList)
            {
                Destroy(obj);
            }
            cardList.Clear();

            Invoke("DrawCards", 1f);
        }

    }

    public void UseCard(Card card, GameObject cardHolder)
    {
        //Play Attack Card
        if (card.mana <= Player.Instance.CurrentMana)
        {
            if (card.attack > 0)
            {
                Debug.Log("using " + card.name);
                if (target.CurrHealth > 0)
                {
                    if (target.IsExposed)
                    {
                        target.CurrHealth -= card.attack * 2;
                        Debug.Log("enemy hp now: " + target.CurrHealth + "... EXPOSED ATTACK(x2)");
                        cardIndex = cardList.IndexOf(cardHolder);
                        RemoveCard(cardHolder);
                    }
                    else
                    {
                        target.CurrHealth -= card.attack;
                        Debug.Log("enemy hp now: " + target.CurrHealth);
                        RemoveCard(cardHolder);
                    }

                    if (target.CurrHealth <= 0)
                    {
                        target.Die();
                    }
                }
                else
                {
                    target.Die();
                }
            }

            //Play Spell (dont put elseif, attack card can also have spells)
            if (card.isSpell)
            {
                if (card.exposedTurns > 0)
                {
                    Debug.Log("applied exposed to target");
                    target.UpdateExposed(card.exposedTurns);
                }

            }
            player.UpdateMana(card);
            UIManager.Instance.UpdateEnemyHP(target);
        }

        else
        {
            Debug.Log("Not Enough mana!");
        }

    }

    void RemoveCard(GameObject cardObject)
    {
        cardIndex = cardList.IndexOf(cardObject);
        Destroy(cardObject);
        cardList.RemoveAt(cardIndex);
    }

}

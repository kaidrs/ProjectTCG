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
    public Stack<Card> Deck { get => deck; set => deck = value; }

    Enemy target;
    Player player;

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardObject;

    [SerializeField] List<Card> deckList = new List<Card>();
    [SerializeField] List<GameObject> cardList = new List<GameObject>();
    Stack<Card> deck;

    int cardIndex;
    const int handSize = 6;

    private void Start()
    {
        player = GetComponent<Player>();
        UpdateTarget();
        ShuffleDeck(deckList);
        Deck = new Stack<Card>(deckList);
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
        GameManager.Instance.EndingTurn = false;
        if (Deck.Count >= handSize)
        {
            for (int i = 0; i < handSize; i++)
            {
                cardObject.GetComponent<CardDisplay>().Card = Deck.Peek();

                cardList.Add(Instantiate(cardObject, handParent.transform));
                Deck.Pop();
            }
            UIManager.Instance.UpdateDeckAmountUI();

        }

        else if (Deck.Count < handSize && Deck.Count > 0)
        {
            Debug.Log("Less than " + handSize + ", currently have " + Deck.Count + "cards in deck");
            //foreach(var obj in deck)
            //{
            //    cardObject.GetComponent<CardDisplay>().Card = deck.Peek();

            //    cardList.Add(Instantiate(cardObject, handParent.transform));
            //    deck.Pop();
            //}

            while (Deck.Count > 0)
            {
                cardObject.GetComponent<CardDisplay>().Card = Deck.Peek();

                cardList.Add(Instantiate(cardObject, handParent.transform));
                Deck.Pop();
            }
            UIManager.Instance.UpdateDeckAmountUI();


        }

        else
        {
            Debug.Log("Deck empty - Cannot draw!");
        }

        //Debug.Log("Current cards left in deck : " + deck.Count);
    }

    void DrawCards(int num)
    {
        if (Deck.Count >= num)
        {
            for (int i = 0; i < num; i++)
            {
                cardObject.GetComponent<CardDisplay>().Card = Deck.Peek();
                Instantiate(cardObject, handParent.transform);
                Deck.Pop();
            }
            UIManager.Instance.UpdateDeckAmountUI();
        }

    }

    //Called by endturn
    public void FlushCards()
    {
        foreach (var obj in cardList)
        {
            Destroy(obj);
        }
        cardList.Clear();

        Invoke("DrawCards", 1f);


    }

    public void UseCard(Card card, GameObject cardHolder)
    {
        //Play Attack Card
        if (card.mana <= Player.Instance.CurrentMana)
        {
            if (card.attack > 0)
            {
                Debug.Log("using " + card.name);
                Enemy.Instance.TakeDamage(card.attack);
                UIManager.Instance.UpdateEnemyHP(target);
            }

            //Play Spell (dont put elseif, attack card can also have spells)
            if (card.isSpell)
            {
                if (card.exposedTurns > 0)
                {
                    Debug.Log("applied exposed to target");
                    target.UpdateExposed(card.exposedTurns);
                }
                if (card.armorValue > 0)
                {
                    Player.Instance.UpdateArmor(card.armorValue);
                    Debug.Log($"Added {card.armorValue} to armor");
                }

            }

            cardIndex = cardList.IndexOf(cardHolder);
            RemoveCard(cardHolder);
            player.UpdateMana(card);

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

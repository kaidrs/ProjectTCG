using System.Collections.Generic;
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
    public List<Card> DiscardPile { get => discardPile; set => discardPile = value; }

    Enemy target;
    Player player;

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardObject;

    [SerializeField] ScriptableObject deckSO;
    Deck deckObject;
    List<Card> deckList = new List<Card>();

    //[SerializeField] List<Card> deckList = new List<Card>();
    [SerializeField] List<GameObject> handList = new List<GameObject>();
    [SerializeField] List<Card> discardPile = new List<Card>();

    Stack<Card> deck;

    int cardIndex;
    const int handSize = 6;

    private void Start()
    {
        player = GetComponent<Player>();
        deckObject = (Deck)deckSO;
        deckList = deckObject.deckList;
        InitHand();
    }

    public void InitHand()
    {
        UpdateTarget();
        ShuffleDeck(deckList);
        FlushCards(false);
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

                handList.Add(Instantiate(cardObject, handParent.transform));
                Deck.Pop();
            }
            UIManager.Instance.UpdateDeckAmountUI();
        }

        else if (Deck.Count < handSize && Deck.Count > 0)
        {
            Debug.Log("Less than " + handSize + ", currently have " + Deck.Count + "cards in deck");

            while (Deck.Count > 0)
            {
                cardObject.GetComponent<CardDisplay>().Card = Deck.Peek();

                handList.Add(Instantiate(cardObject, handParent.transform));
                Deck.Pop();
            }
            UIManager.Instance.UpdateDeckAmountUI();
        }
        else
        {
            Debug.Log("Deck empty - Cannot draw!");
        }
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
    public void FlushCards(bool draw)
    {
        foreach (var obj in handList)
        {
            Destroy(obj);
        }
        handList.Clear();
        if (draw) Invoke("DrawCards", 1f);

    }

    public void UseCard(Card card, GameObject cardHolder)
    {
        if (card.mana <= Player.Instance.CurrentMana)
        {
            card.UseCard();
            DiscardPile.Add(card);
            RemoveCard(cardHolder);
            player.UpdateMana(card);
            cardIndex = handList.IndexOf(cardHolder); 
        }

        else
        {
            Debug.Log("Not Enough mana!");
        }

    }

    void RemoveCard(GameObject cardObject)
    {
        cardIndex = handList.IndexOf(cardObject);
        Destroy(cardObject);
        handList.RemoveAt(cardIndex);
    }

}

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

    public List<Card> CardList { get => cardList; set => cardList = value; }

    Enemy target;
    Player player;

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardObject;

    [SerializeField] List<Card> cardList = new List<Card>();
    Stack<Card> deck;

   
    private void Start()
    {
        player = GetComponent<Player>();
        UpdateTarget();
        ShuffleDeck(cardList);
        deck = new Stack<Card>(cardList);
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
        for(int i = 0; i < 6; i++)
        {
            cardObject.GetComponent<CardDisplay>().Card = deck.Peek();
            Instantiate(cardObject, handParent.transform);
            deck.Pop();
        }
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

    public void UseCard(Card card, GameObject cardHolder)
    {
        //Play Attack Card
        if (card.mana <= Player.Instance.CurrentMana)
        {
            if (card.attack > 0)
            {
                Debug.Log("using " + card.name);
                if(target.CurrHealth > 0)
                {
                    if (target.IsExposed)
                    {
                        target.CurrHealth -= card.attack * 2;
                        Debug.Log("enemy hp now: " + target.CurrHealth + "... EXPOSED ATTACK(x2)");
                        Destroy(cardHolder);
                        target.UpdateExposed();
                    }
                    else
                    {
                        target.CurrHealth -= card.attack;
                        Debug.Log("enemy hp now: " + target.CurrHealth);
                        Destroy(cardHolder);
                    }

                    if(target.CurrHealth <= 0)
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
}

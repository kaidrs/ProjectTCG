using System.Collections;
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

    public List<Card> CardList { get => cardList; set => cardList = value; }

    Enemy target;
    Player player;
    [SerializeField] List<Card> cardList = new List<Card>();

    private void Start()
    {
        player = GetComponent<Player>();
        UpdateTarget();
    }

    void UpdateTarget()
    {
        target = FindObjectOfType<Enemy>();
    }

    void DrawCards()
    {

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

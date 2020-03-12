using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDeck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] TextMeshProUGUI deckText;
    List<string> cardTextObjects;
    string deckNames;
    Stack<Card> deck;

    void Start()
    {
        deck = new Stack<Card>(CardManager.Instance.CardList);

        //Build Deck Hover UI
        cardTextObjects = new List<string>();
        foreach (var ob in deck)
        {
            cardTextObjects.Add(ob.name);
        }
        deckNames = "Deck Contains: \n" + string.Join("\n", cardTextObjects);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            deckText.text = deckNames;
            //foreach(var obj in deck)
            //{
            //    Debug.Log(obj.ToString());


            //}
            
        }

       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }


}

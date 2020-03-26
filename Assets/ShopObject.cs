using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField] Card shopObjectCard;
    [SerializeField] Image shopObjectImage;
    [SerializeField] TextMeshProUGUI shopObjectGoldText;
    int cardCost;

    public Card ShopObjectCard { get => shopObjectCard; set => shopObjectCard = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (HasMoney(cardCost))
        {
            CardManager.Instance.DeckList.Add(shopObjectCard);
            this.GetComponent<Button>().interactable = false;
            Player.Instance.Gold -= cardCost;
            UIManager.Instance.ShopPlayerGoldText.text = Player.Instance.Gold.ToString();
        }
        else
        {
            UIManager.Instance.ShopObjectName.text = "Not enough gold!";
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShopObjectName.text = shopObjectCard.name;
        UIManager.Instance.ShopObjectDescription.text = shopObjectCard.desc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    void Start()
    {
        shopObjectImage.sprite = shopObjectCard.art;
        cardCost = Random.Range(25, 50);
        shopObjectGoldText.text = cardCost.ToString();
    }


    private bool HasMoney(int cardCost)
    {
        if(Player.Instance.Gold > cardCost)
        {
            return true;
        }
        return false;
    }
}

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

    public Card ShopObjectCard { get => shopObjectCard; set => shopObjectCard = value; }

    public void OnPointerClick(PointerEventData eventData)
    {

        CardManager.Instance.DeckList.Add(shopObjectCard);
        this.GetComponent<Button>().interactable = false;
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
        shopObjectGoldText.text = Random.Range(1, 100).ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

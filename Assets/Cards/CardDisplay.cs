using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Card card;
    [SerializeField] Image cardArt;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;
    RectTransform rect;

    public Card Card { get => card; set => card = value; }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        cardArt.sprite = Card.art;
        manaText.text = Card.mana.ToString();
        nameText.text = Card.name;
        descText.text = Card.desc;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GetComponentInParent<GridLayoutGroup>().padding.top = 745;
        }

        LayoutRebuilder.MarkLayoutForRebuild(rect);
        //Debug.Log("Mouse over: " + card.name);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GetComponentInParent<GridLayoutGroup>().padding.top = 1225;
        }

        LayoutRebuilder.MarkLayoutForRebuild(rect);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            CardManager.Instance.UseCard(Card, this.gameObject);
        }
        
    }
}

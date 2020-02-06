using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Card card;
    [SerializeField] Image cardArt;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI hpText;
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        cardArt.sprite = card.art;
        manaText.text = card.mana.ToString();
        attackText.text = card.attack.ToString();
        hpText.text = card.hp.ToString();
        nameText.text = card.name;
        descText.text = card.desc;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GetComponentInParent<GridLayoutGroup>() != null)
        {
            GetComponentInParent<GridLayoutGroup>().padding.top = 300;
        }

        LayoutRebuilder.MarkLayoutForRebuild(rect);
        Debug.Log("Mouse over: " + card.name);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponentInParent<GridLayoutGroup>() != null)
        {
            GetComponentInParent<GridLayoutGroup>().padding.top = 675;
        }

        LayoutRebuilder.MarkLayoutForRebuild(rect);
    }
}

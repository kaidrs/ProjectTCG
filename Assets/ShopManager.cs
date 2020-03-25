using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopObjectPrefab;
    [SerializeField] GameObject shopObjectParent;

    [SerializeField] List<Card> shopObjectList;
    Card generatedCard;


    const int numShopObjects = 5;
    System.Random rand = new System.Random();
    int r;


    // Start is called before the first frame update
    void Start()
    {
        GenerateShopNodes();
    }

    Card GetRandomShopObject()
    {
        r = rand.Next(shopObjectList.Count);
        return shopObjectList.ElementAt(r);
    }

    public void GenerateShopNodes()
    {
        for(int i = 0; i < numShopObjects; i++)
        {
            GameObject clone = Instantiate(shopObjectPrefab, shopObjectParent.transform);
            clone.GetComponent<ShopObject>().ShopObjectCard = GetRandomShopObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

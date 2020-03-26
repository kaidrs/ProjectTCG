using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Singleton
    private static ShopManager instance = null;
    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ShopManager>();
            }
            return instance;
        }
    }
    #endregion

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
        //OpenShop();
    }

    public void OpenShop()
    {
        GenerateShopNodes();
        UIManager.Instance.ShopPlayerGoldText.text = Player.Instance.Gold.ToString();
        UIManager.Instance.ShowShopPanel(true);
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

    public void LeaveShop()
    {
        //LevelManager.Instance.LevelIndex++;
        UIManager.Instance.ShowShopPanel(false);
        LevelManager.Instance.JoinLevel(LevelManager.Instance.LevelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

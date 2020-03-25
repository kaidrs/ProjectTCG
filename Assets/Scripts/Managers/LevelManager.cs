using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager instance = null;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }
    #endregion

    [Header("Object References")]
    [SerializeField] GameObject firstPathParent;
    [SerializeField] GameObject secondPathParent;
    [SerializeField] GameObject levelNode;
    [SerializeField] GameObject shopNode;
    List<GameObject>[] levelObjects = new List<GameObject>[2];

    [Header("Key Values")]
    [SerializeField] int levelIndex = 0;
    [SerializeField] int minLengthOfPath = 8;
    [SerializeField] int maxLengthOfPath = 11;

    int randomLength;
    int numShops;
    int chanceToShop;
    int shopWeight = 5;
    int firstPathLength;

    public int LevelIndex { get => levelIndex; set => levelIndex = value; }

    public void JoinLevel(int index)
    {
        UIManager.Instance.FadeOutLevelPanel();
        CardManager.Instance.InitHand();
        GameManager.Instance.CreateEnemy(index);
        GameManager.Instance.ResetLevel();
        LevelIndex++;
        foreach(var lvlObj in levelObjects[0])
        {
            lvlObj.GetComponent<Button>().interactable = false;
        }
        foreach (var lvlObj in levelObjects[1])
        {
            lvlObj.GetComponent<Button>().interactable = false;
        }
        levelObjects[0].ElementAt(levelIndex).GetComponent<Button>().interactable = true; 
    }

    int CalculateNumShops()
    {
        int rand = Random.Range(2, 5);
        return rand;
    }

    //Objects add themself to respective lists in firstPath & secondPath
    //Player chooses list, only first button on both paths is interactable
    //Create at least 1 shop per path rule, with possibility of ~3
    void AssignLevelNode(int listIndex, GameObject pathParent)
    {
        randomLength = Random.Range(minLengthOfPath, maxLengthOfPath);
        numShops = CalculateNumShops();

        for (int i = 0; i < randomLength; i++)
        {
            if (numShops > 0 && i > 2) //only spawn shops after spawning 2 levelNodes minumum
            {
                chanceToShop = Random.Range(1, shopWeight); //shopweight , 1/5(-1) chance to spawn shop, if not shop, reduce shopweight to increase chance of spawn. then reset weight
                if (chanceToShop == 1)
                {
                    levelObjects[listIndex].Add(Instantiate(shopNode, pathParent.transform));
                    numShops--;
                    shopWeight = 5;
                    Debug.Log($"Adding shop, left to add: {numShops}");
                }
                else
                {
                    levelObjects[listIndex].Add(Instantiate(levelNode, pathParent.transform));
                    shopWeight--;
                }
            }
            else
            {
                levelObjects[listIndex].Add(Instantiate(levelNode, pathParent.transform));
            }
        }
        levelObjects[listIndex].ElementAt(levelIndex).GetComponent<Button>().interactable = true;
    }

    void PopulateLists()
    {
        for (int i = 0; i < levelObjects.Length; i++)
        {
            levelObjects[i] = new List<GameObject>();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        PopulateLists();
        AssignLevelNode(0, firstPathParent);
        AssignLevelNode(1, secondPathParent);
    }



    // Update is called once per frame
    void Update()
    {

    }
}

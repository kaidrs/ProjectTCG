using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Singleton
    private static Player instance = null;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    #endregion


    public int CurrentMana { get => currentMana; set => currentMana = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }

    int maxMana = 5;
    int currentMana = 5;


    public void UpdateMana(Card card)
    {
        CurrentMana -= card.mana;
        UIManager.Instance.UpdateManaUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

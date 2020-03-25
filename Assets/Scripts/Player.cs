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
    public float CurrHealth { get => currHealth; set => currHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float TotalArmor { get => totalArmor; set => totalArmor = value; }
    public bool AttackArmor { get => attackArmor; set => attackArmor = value; }
    public int AaDmgVal { get => aaDmgVal; set => aaDmgVal = value; }

    [SerializeField] int maxMana = 5;
    int currentMana = 5;

    [SerializeField] float maxHealth = 50;
    float currHealth = 50;
    float totalArmor = 0;

    bool attackArmor;
    int aaNumTurns;
    int aaDmgVal;
    int statusIndex;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    public void ClearStatusEffects()
    {
        UIManager.Instance.ClearPlayerStatusUI();
        attackArmor = false; //Enum of bool / array of bool , set all false
        aaNumTurns = 0;
        statusIndex = 0;
    }

    int GetStatusIndex()
    {
        for (int i = 0; i < 5; i++)
        {
            if (UIManager.Instance.PlayerStatusEffects[i].gameObject.tag == "AttackArmor")
            {
                statusIndex = i;
            }
        }
        return statusIndex;
    }

    public void InitAttackArmor(int numTurns, int damageVal)
    {
        statusIndex = 0;
        attackArmor = true;
        aaNumTurns = numTurns;
        aaDmgVal = damageVal;

        statusIndex = GetStatusIndex();

        UIManager.Instance.PlayerStatusEffects[statusIndex].SetActive(true);
        UIManager.Instance.PlayerStatusText[statusIndex].text = aaNumTurns.ToString();
    }

    public void CalcAttackArmor()
    {
        if(aaNumTurns > 1)
        {
            aaNumTurns--;
            statusIndex = GetStatusIndex();
            UIManager.Instance.PlayerStatusText[statusIndex].text = aaNumTurns.ToString();
            Debug.Log(aaNumTurns);
        }
        else
        {
            UIManager.Instance.PlayerStatusEffects[0].SetActive(false);
            attackArmor = false;
        }

    }

    public void TakeDamage(int dmgVal)
    {
        Debug.Log($"Armor {TotalArmor}");

        if (TotalArmor < 0)
        {
            CurrHealth -= dmgVal;
        }
        else if (TotalArmor > dmgVal)
        {
            TotalArmor -= dmgVal;
            UIManager.Instance.UpdatePlayerArmor();
        }
        else
        {
            TotalArmor -= dmgVal;
            CurrHealth += TotalArmor;
            TotalArmor = 0;
            UIManager.Instance.TogglePlayerArmor(false);

        }

        Debug.Log($"Armor {TotalArmor}");
    }

    public void UpdateArmor(int armorVal)
    {
        if(TotalArmor == 0)
        {
            TotalArmor += armorVal;
            UIManager.Instance.TogglePlayerArmor(true);
        }
        else
        {
            TotalArmor += armorVal;
            UIManager.Instance.UpdatePlayerArmor();
        }

    }

    public void UpdateMana(Card card)
    {
        CurrentMana -= card.mana;
        UIManager.Instance.UpdateManaUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMana = MaxMana;
        currHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

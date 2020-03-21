using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    #endregion

    public TextMeshProUGUI PlayerMana { get => playerMana; set => playerMana = value; }
    public GameObject LevelPanel { get => levelPanel; set => levelPanel = value; }
    public GameObject FadeLevelPanel { get => fadeLevelPanel; set => fadeLevelPanel = value; }
    public GameObject[] EnemyStatusIndicator { get => enemyStatusIndicator; set => enemyStatusIndicator = value; }
    public GameObject[] PlayerStatusEffects { get => playerStatusEffects; set => playerStatusEffects = value; }
    public TextMeshProUGUI[] PlayerStatusText { get => playerStatusText; set => playerStatusText = value; }
    public TextMeshProUGUI PlayerHealthText { get => playerHealthText; set => playerHealthText = value; }
    public TextMeshProUGUI EnemyHealthText { get => enemyHealthText; set => enemyHealthText = value; }

    [Header("Player UI")]
    [SerializeField] Slider playerHpSlider;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] GameObject playerArmorImage;
    [SerializeField] TextMeshProUGUI playerArmorText;
    [SerializeField] TextMeshProUGUI playerMana;
    [SerializeField] TextMeshProUGUI playerDeckAmt;
    [SerializeField] GameObject[] playerStatusEffects;
    [SerializeField] TextMeshProUGUI[] playerStatusText;

    [Header("Enemy UI")]
    [SerializeField] Slider enemyHpSlider;
    [SerializeField] TextMeshProUGUI enemyHealthText;
    [SerializeField] GameObject[] enemyStatusIndicator;
    // Start is called before the first frame update

    [Header("Level PostGame")]
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject fadeLevelPanel;

    public void ClearPlayerStatusUI()
    {
        foreach (var effect in playerStatusEffects)
        {
            effect.gameObject.SetActive(false);//move to uimanager
        }
    }

    public void UpdateDeckAmountUI()
    {
        playerDeckAmt.text = CardManager.Instance.Deck.Count.ToString();
    }

    public void UpdateManaUI()
    {
        PlayerMana.text = Player.Instance.CurrentMana.ToString() + "/" + Player.Instance.MaxMana.ToString();
    }

    public void UpdatePlayerHp()
    {
        playerHpSlider.value = Player.Instance.CurrHealth / Player.Instance.MaxHealth;
        playerHealthText.text = Player.Instance.CurrHealth.ToString() + "/"  +  Player.Instance.MaxHealth.ToString();
    }
    public void UpdatePlayerArmor()
    {
        playerArmorText.text = Player.Instance.TotalArmor.ToString();
    }

    public void UpdateEnemyHP()
    {
        enemyHpSlider.value = Enemy.Instance.CurrHealth / Enemy.Instance.MaxHealth;
        enemyHealthText.text = Enemy.Instance.CurrHealth.ToString() + "/" + Enemy.Instance.MaxHealth.ToString();
    }

    //Animation events
    public void FadeInLevelPanel()
    {
        fadeLevelPanel.SetActive(true);
        Invoke("ShowLevelPanel", 1.5f);
    }

    public void FadeOutLevelPanel()
    {
        fadeLevelPanel.SetActive(false);
        levelPanel.SetActive(false);
    }
    void ShowLevelPanel()
    {
        levelPanel.SetActive(true);
        //fadeLevelPanel.SetActive(false);
    }

    public void TogglePlayerArmor(bool enabled)
    {
        playerArmorImage.SetActive(enabled);
        UpdatePlayerArmor();
    }



    void Start()
    {
        UpdateManaUI();
        UpdatePlayerHp();
        UpdateEnemyHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
